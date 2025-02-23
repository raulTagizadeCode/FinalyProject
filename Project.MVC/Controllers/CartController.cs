using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.BL.DTOs.BasketItemDTOs;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFoodService _foodservice;
        readonly UserManager<AppUser> _usermanager;
        readonly IStripeService _stripeservice;
        readonly IOrderService _orderservice;
        public CartController(AppDbContext context, IFoodService foodservice, UserManager<AppUser> usermanager, IStripeService stripeservice, IOrderService orderservice)
        {
            _context = context;
            _foodservice = foodservice;
            _usermanager = usermanager;
            _stripeservice = stripeservice;
            _orderservice = orderservice;
        }

        public IActionResult Index()
        {
            BasketDto basket = GetBasket();

            return View(basket);
        }
        public async Task<IActionResult> AddToBasket(int productId)
        {

            if (User.Identity.IsAuthenticated)
            {
                AppUser? user = await _usermanager.FindByNameAsync(User.Identity.Name);
                if (user == null) { return NotFound(); }
                Food? food = await _context.Foods.FindAsync(productId);
                if (food == null) { return NotFound(); }
                BasketItem? basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.FoodId == productId && b.AppuserId == user.Id);
                if (basketItem == null)
                {
                    BasketItem item = new BasketItem();
                    item.FoodId = productId;
                    item.AppuserId = user.Id;
                    item.Quantity = 1;
                    item.Price = food.Price;
                    item.CreatedAt = DateTime.Now;
                    await _context.BasketItems.AddAsync(item);
                }
                else
                {
                    basketItem.Quantity++;
                }
                await _context.SaveChangesAsync();
            }

            Food? product = _context.Foods.Find(productId);
            if (product == null)
            {
                return NotFound("tapilmadi");
            }
            var cookieOption = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            BasketDto basket = GetBasket();

            if (basket == null)
            {
                basket = new BasketDto();
            }
            BasketItemDto? existingBasketItem = basket.Items.FirstOrDefault(g => g.Id == product.Id);
            if (existingBasketItem == null)
            {

                BasketItemDto basketItemDto = new BasketItemDto()
                {
                    Description = product.Description,
                    Id = product.Id,
                    ImageUrl = product.ImageUrl,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Quantity = 1

                };
                basket.Items.Add(basketItemDto);
            }
            else
            {
                existingBasketItem.Quantity += 1;

            }
            var cookieBasket = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", cookieBasket, cookieOption);
            return RedirectToAction("Index", "Menu");
        }
        public BasketDto GetBasket()
        {
            var basket = Request.Cookies["Basket"];
            if (basket != null)
            {
                BasketDto? existingBasket = JsonConvert.DeserializeObject<BasketDto>(basket);
                return existingBasket;

            }
            return new BasketDto();
        }
        [HttpPost]

        public IActionResult RemoveFromBasket(int productId)
        {
            var basket = GetBasket();
            if (basket == null || basket.Items.Count == 0)
            {
                return NotFound("Basket boşdur və ya tapılmadı.");
            }

            var itemToRemove = basket.Items.FirstOrDefault(g => g.Id == productId);
            if (itemToRemove != null)
            {
                basket.Items.Remove(itemToRemove);
                var cookieOption = new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true
                };
                var cookieBasket = JsonConvert.SerializeObject(basket);
                Response.Cookies.Append("Basket", cookieBasket, cookieOption);
                return Ok();
            }

            return NotFound("Məhsul tapılmadı.");
        }

        [HttpPost]

        public async Task<IActionResult> Checkout()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return NotFound();

            // İstifadəçinin səbətində olan məhsulları əldə edirik
            List<BasketItem> basketItems = await _context.BasketItems
                .Where(b => b.AppuserId == user.Id && b.OrderId == null)
                .ToListAsync();

            if (!basketItems.Any()) return BadRequest("Səbət boşdur!");

            // Toplam məbləği səbətdə olan məhsulların qiymətinə əsasən hesablayırıq
            double totalAmount = basketItems.Sum(item => item.Price * item.Quantity) + 5; // 5 EUR çatdırılma haqqı

            // Stripe üçün checkout sessiyası yaradırıq
            string sessionUrl = await _stripeservice.CreateCheckoutSession(totalAmount, "eur");

            return Ok(new { url = sessionUrl });
        }


        public async Task<IActionResult> PaymentSuccess(OrderCreateDto dto)
        {

            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Message", "Rayting");

            // İstifadəçinin səbətindəki məhsulları əldə edirik
            List<BasketItem> basketItems = await _context.BasketItems
                .Where(b => b.AppuserId == user.Id && b.OrderId == null)
                .ToListAsync();

            if (!basketItems.Any()) return RedirectToAction("Index", "Menu");

            // Yeni sifariş yaradılır
            var order = new Order
            {
                AppUserId = user.Id,
                Status = OrderStatus.wasNoted,
                CreatedAt = DateTime.Now,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync(); // Sifarişin Id-si yaransın

            // BasketItem-lərin OrderId sahəsini yeniləyirik
            foreach (var item in basketItems)
            {
                item.OrderId = order.Id; // Burada sadəcə OrderId-ni təyin edirik, Id yox!
                _context.BasketItems.Update(item); // Entity State-i Modified olmalıdır
            }

            await _context.SaveChangesAsync(); // Yenilənmiş OrderId ilə yenidən bazaya yazırıq

            // Cookie-ni keçmiş tarixə təyin edərək sil
            Response.Cookies.Append("Basket", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Keçmiş tarix verərək silirik
                Path = "/" // Bütün yol üçün silinsin
            });

            return View(); // Ödəniş uğurlu səhifəsinə yönləndir
        }
        public IActionResult PaymentFailed()
        {
            return View(); // Ödəniş uğursuz oldu səhifəsinə yönləndir
        }

        public async Task<IActionResult> Sifaris()
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return NotFound();

            var orders = await _context.Orders
                .Where(o => o.AppUserId == user.Id)
                .Include(o => o.BasketItems)
                .ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _usermanager.GetUserAsync(User);
            if (user == null) return NotFound();

            var order = await _context.Orders
                .Where(o => o.Id == id && o.AppUserId == user.Id)
                .Include(o => o.BasketItems)
                .ThenInclude(b => b.Food) // Yemək məlumatlarını gətiririk
                .FirstOrDefaultAsync();

            if (order == null) return NotFound();

            return View(order);
        }
        public IActionResult OrderDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderDetails(OrderCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Location) || dto.OrderCategoryId == 0)
            {
                ModelState.AddModelError("", "Bütün sahələri doldurun!");
                return View(dto);
            }

            // Məlumatları Session və ya TempData ilə saxlayırıq
            TempData["OrderCategoryId"] = dto.OrderCategoryId;
            TempData["Location"] = dto.Location;

            return RedirectToAction("Index", "Cart");
        }

    }
}