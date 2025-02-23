using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.BL.DTOs.TableCategoryPlaceDTOs;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.MVC.Controllers
{
    public class ReservationController : Controller
    {
        readonly IReservationService _reservationService;
        readonly UserManager<AppUser> _userManager;
        readonly AppDbContext _context;
       
        public ReservationController(UserManager<AppUser> userManager, IReservationService reservationService, AppDbContext context)
        {
            _userManager = userManager;
            _reservationService = reservationService;
            _context = context;
        }



        public async Task<IActionResult> Index(int categoryId = 1, int placeId = 1)
        {
            var tableCategories = _context.TableCategoryNumbers
                .Select(c => new GetTableCategoryNumberDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            var tablePlaces = _context.TableCategoryPlaces
                .Select(c => new GetTableCategoryPlaceDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            // ViewData vasitəsilə View-ə göndər
            ViewData["TableCategoryNumber"] = new SelectList(tableCategories, "Id", "Name");
            ViewData["TableCategoryPlace"] = new SelectList(tablePlaces, "Id", "Name");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Aktiv masaları gətir
            var tables = await _reservationService.GetAvailableTablesAsync(categoryId, placeId);

            return View(tables);
        }


        public IActionResult Reserve()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Reserve(int tableId, string reservationTime, int personCount,bool confirmation)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            if (!TimeSpan.TryParse(reservationTime, out TimeSpan parsedTime))
            {
                TempData["Error"] = "Yanlış saat formatı!";
                return RedirectToAction("Index");
            }
            confirmation = true;
            // 19:00 - 23:00 aralığında olmalıdır
            if (parsedTime < TimeSpan.FromHours(19) || parsedTime > TimeSpan.FromHours(23))
            {
                TempData["Error"] = "Yalnız 19:00 - 23:00 arasında rezerv edə bilərsiniz.";
                return RedirectToAction("Index");
            }

            var success = await _reservationService.ReserveTableAsync(tableId, user.Id, parsedTime, personCount,true);
            if (!success)
            {
                TempData["Error"] = "Siz artıq bu gün üçün bir masa rezerv etmisiniz.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Masa uğurla rezerv edildi.";
            return RedirectToAction("Index");
        }


    }
}
