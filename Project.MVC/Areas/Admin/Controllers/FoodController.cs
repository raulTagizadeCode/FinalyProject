using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Models;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class FoodController : Controller
    {
        readonly IFoodService _foodService;
        readonly ICategoryService _categoryService;

        public FoodController(ICategoryService categoryService, IFoodService foodService)
        {
            _categoryService = categoryService;
            _foodService = foodService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<GetFoodDto> list = await _foodService.GetAllAsync();

                return View(list);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        public async Task<IActionResult> Create()
        {
           
            try
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetCategoryListItemsAsync(), "Id", "Name");
                return View();  
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFoodDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return View(dto);
            }

            try
            {
                await _foodService.CreateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return View(await _foodService.GetByIdForUpdateAsync(id));

            }
            catch (BaseException ex)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateFoodDto dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            //    return View(dto);
            //}
           
            try
            {
                await _foodService.UpdateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["Category"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _foodService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _foodService.GetByIdWithChildrenAsync(id));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

    }
}
