using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.MVC.Controllers
{
    public class MenuController : Controller
    {
        readonly ICategoryService _service;
        readonly IFoodService _foodService;
        readonly AppDbContext _appDbContext;
        public MenuController(ICategoryService service, IFoodService foodService, AppDbContext appDbContext)
        {
            _service = service;
            _foodService = foodService;
            _appDbContext = appDbContext;
        }

        public IActionResult Index(string searchTerm, double? minPrice, double? maxPrice, int? categoryId, int id)
        {
            var categories = _appDbContext.Categories
                .Select(c => new GetCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            var productsQuery = _appDbContext.Foods.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(f =>
                    f.Name.Contains(searchTerm) ||
                    f.Category.Name.Contains(searchTerm) ||
                    (searchTerm.Length == 1 && f.Name.Length >= 2 && f.Name.Substring(0, 2).Contains(searchTerm)) ||
                    (searchTerm.Length == 2 && f.Name.Length >= 3 && f.Name.Substring(0, 3).Contains(searchTerm)) ||
                    f.Name.EndsWith(searchTerm));
            }

            if (minPrice.HasValue)
                productsQuery = productsQuery.Where(f => f.Price >= minPrice);
            if (maxPrice.HasValue)
                productsQuery = productsQuery.Where(f => f.Price <= maxPrice);
            if (categoryId.HasValue)
                productsQuery = productsQuery.Where(f => f.CategoryId == categoryId);

            var products = productsQuery
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    f.Price,
                    f.ImageUrl,
                    f.CategoryId
                }).ToList();

            // Uyğun olan yeməklərin adlarını axtarış üçün göstərmək
            var suggestedNames = productsQuery
                .Select(f => f.Name)
                .Distinct()
                .Take(10) // Maksimum 10 nəticə göstər
                .ToList();

            ViewBag.Products = products;
            ViewBag.Suggestions = suggestedNames; // Axtarış təklifləri üçün

            return View(categories);
        }






        public async Task<IActionResult> Details(int id)
        {
            var res = await _foodService.GetByIdAsync(id);
            return View(res);
        }
    }
}
