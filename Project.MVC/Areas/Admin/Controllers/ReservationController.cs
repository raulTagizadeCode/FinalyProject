using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Enums;
using Project.DAL.Models;

namespace Project.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReservationController : Controller
    {
        readonly AppDbContext _context;
        readonly IReservationService _reservationService;
        public ReservationController(AppDbContext context, IReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var res = _context.Reservations.ToList();
            return View(res);
        }
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                return View(await _reservationService.GetByIdWithChildrenAsync(id));
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
        public async Task<IActionResult> Update(int id)
        {

            try
            {
                return View(await _context.Reservations.FindAsync(id));

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
        public async Task<IActionResult> Update(Reservation dto)
        {
            int id = dto.Id;
            var res = await _context.Reservations.FindAsync(id);
            var application = _context.Reservations
                .AsNoTracking() // Entity-in izlənməsini dayandırırıq
                .FirstOrDefault(a => a.Id == dto.Id);

            if (application == null) return NotFound();

            // Yalnız Status sahəsini yenilə
            application.confirmation = dto.confirmation;
            _context.Reservations.Update(application); // Bütün obyekt yenilənir, amma yalnız Status dəyişəcək
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Masa");
        }
    }
}
