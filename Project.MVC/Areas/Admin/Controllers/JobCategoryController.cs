using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.JobCategoryDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobCategoryController : Controller
    {
       readonly IJobCategoryService _categoryService;
        public JobCategoryController(IJobCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<GetJobCategoryDto> list = await _categoryService.GetAllAsync();

            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _categoryService.CreateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                return View(await _categoryService.GetByIdForUpdateAsync(id));

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateJobCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategoryDto);
            }
            await _categoryService.UpdateAsync(updateCategoryDto);
            return RedirectToAction("Index", "JobCategory");
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
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
                return View(await _categoryService.GetByIdWithChildrenAsync(id));
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
