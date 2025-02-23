using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.MasaDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MasaController : Controller
    {
        readonly IMasaService _masaService;
        readonly ITableCategoryPlaceService _tableCategoryPlaceService;
        readonly ITableCategoryNumberService _tableCategoryNumberService;
        readonly IRepository<Masa> _repository;
        readonly AppDbContext _context;
        readonly IRepository<Reservation> _reservationRepository;
        public MasaController(IMasaService masaService, ITableCategoryPlaceService tableCategoryPlaceService, ITableCategoryNumberService tableCategoryNumberService, IRepository<Masa> repository, AppDbContext context, IRepository<Reservation> reservationRepository)
        {
            _masaService = masaService;
            _tableCategoryPlaceService = tableCategoryPlaceService;
            _tableCategoryNumberService = tableCategoryNumberService;
            _repository = repository;
            _context = context;
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<GetMasaDto> list = await _masaService.GetAllAsync();

                return View(list);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        public async Task<IActionResult> Active()
        {
            await _masaService.ActiveAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> FalseReserv(Reservation reservation)
        {
            int id = reservation.Id;
            var res = await _reservationRepository.GetByIdAsync(id);
            var application = _context.Reservations
                .AsNoTracking() // Entity-in izlənməsini dayandırırıq
                .FirstOrDefault(a => a.Id == reservation.Id);

            if (application == null) return NotFound();

            // Yalnız Status sahəsini yenilə
            application.confirmation = reservation.confirmation;
            await _context.SaveChangesAsync();
            _context.Reservations.Update(application); // Bütün obyekt yenilənir, amma yalnız Status dəyişəcək
            return RedirectToAction("Index", "Masa");
        }

        public async Task<IActionResult> TrueMasa(Masa masa )
        {
            try
            {
                var masaa = await _repository.GetByIdAsync(masa.Id);
                if (masaa == null)
                    return NotFound("Masa tapılmadı.");
                masaa.IsActive = true;
                await _repository.UpdateAsync(masaa);
                return RedirectToAction("Index", "Masa");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
                return StatusCode(500, "Server xətası");
            }
        }
        public async Task<IActionResult> Create()
        {

            try
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMasaDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                return View(dto);
            }

            try
            {
                await _masaService.CreateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                return View(await _masaService.GetByIdForUpdateAsync(id));

            }
            catch (BaseException ex)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateMasaDto dto)
        {
           

            try
            {
                await _masaService.UpdateAsync(dto);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["TableCategoryNumber"] = new SelectList(await _tableCategoryNumberService.GetCategoryListItemsAsync(), "Id", "Name");
                ViewData["TableCategoryPlace"] = new SelectList(await _tableCategoryPlaceService.GetCategoryListItemsAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _masaService.DeleteAsync(id);
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
                var masa = await _masaService.GetByIdWithChildrenAsync(id);

                if (masa == null)
                {
                    return NotFound("Masa not found");
                }

                // Yalnız IsActive = true olan rezervasiyaları seçirik
                masa.Reservations = masa.Reservations.Where(r => r.confirmation=true).ToList();

                return View(masa);
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
