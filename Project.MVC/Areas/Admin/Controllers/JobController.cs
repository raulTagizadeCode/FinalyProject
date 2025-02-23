using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.DTOs.JobDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Enums;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JobController : Controller
    {
        readonly AppDbContext _context;
        readonly IJobService _jobService;
        readonly IJobCategoryService _categoryService;
        readonly IJobApplicationService _jobApplicationService;
        public JobController(IJobService jobService, IJobCategoryService categoryService, AppDbContext context, IJobApplicationService jobApplicationService)
        {
            _jobService = jobService;
            _categoryService = categoryService;
            _context = context;
            _jobApplicationService = jobApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<GetJobDto> list = await _jobService.GetAllAsync();

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
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetCategoryListItemsAsync(), "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobDto dto)
        {
          

            try
            {
                await _jobService.CreateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return View(await _jobService.GetByIdForUpdateAsync(id));

            }
            catch (BaseException ex)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");

                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateJobDto dto)
        {
           
            try
            {
                await _jobService.UpdateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["JobCategories"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _jobService.DeleteAsync(id);
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
                return View(await _jobService.GetByIdWithChildrenAsync(id));
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
