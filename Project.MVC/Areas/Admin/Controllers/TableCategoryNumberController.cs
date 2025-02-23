using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class TableCategoryNumberController : Controller
    {
        readonly ITableCategoryNumberService _tableCategoryNumberService;

        public TableCategoryNumberController(ITableCategoryNumberService tableCategoryNumberService)
        {
            _tableCategoryNumberService = tableCategoryNumberService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GetTableCategoryNumberDto> list = await _tableCategoryNumberService.GetAllAsync();

            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTableCategoryNumberDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await _tableCategoryNumberService.CreateAsync(dto);
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
                return View(await _tableCategoryNumberService.GetByIdForUpdateAsync(id));

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
        public async Task<IActionResult> Update(UpdateTableCategoryNumberDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategoryDto);
            }
            await _tableCategoryNumberService.UpdateAsync(updateCategoryDto);
            return RedirectToAction("Index", "TableCategoryNumber");
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tableCategoryNumberService.DeleteAsync(id);
                return RedirectToAction("Index", "TableCategoryNumber");
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
                return View(await _tableCategoryNumberService.GetByIdWithChildrenAsync(id));
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
