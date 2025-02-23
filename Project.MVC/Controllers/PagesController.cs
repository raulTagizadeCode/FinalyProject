using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.Abstractions;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.MVC.Controllers
{
    public class PagesController : Controller
    {
        readonly IJobService _service;
        readonly IJobApplicationService _jobappservice;
        readonly IEmailService _emailservice;
        readonly AppDbContext _context;
        readonly IRaytingService _raytingService;
        public PagesController(IJobService service, IJobApplicationService jobappservice, IEmailService emailservice, AppDbContext context, IRepository<Rayting> repository, IRaytingService raytingService)
        {
            _service = service;
            _jobappservice = jobappservice;
            _emailservice = emailservice;
            _context = context;
            _raytingService = raytingService;
        }


        public IActionResult Confirmation()
        {
            return View();
        }
        public async Task<IActionResult> OurTeam()
        {
            var res = await _jobappservice.GetAllAsync();
            return View(res);
        }
        public async Task<IActionResult> Testimonial()
        {
            var res = await _raytingService.GetAllAsync();
            return View(res);
        }
        public async Task<IActionResult> vacancies()
        {
            var res = await _service.GetAllAsync();
            return View(res);
        }

        public async Task<IActionResult> etrafli(int id)
        {
            try
            {
                return View(await _service.GetByIdWithChildrenAsync(id));
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
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["Job"] = new SelectList(await _service.GetJobListItemsAsync(), "Id", "Name");
                return View();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateJobApplicationDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Job"] = new SelectList(await _service.GetAllAsync(), "Id", "Name");
                return View(dto);
            }

            try
            {
                await _jobappservice.CreateAsync(dto);
                _emailservice.VacancyMessage(dto.UserEmail);
                return RedirectToAction("Index", "Home");
            }
            catch (BaseException ex)
            {
                ViewData["Job"] = new SelectList(await _service.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", ex.Message);
                return View(dto);
            }
            catch (Exception)
            {
                ViewData["Job"] = new SelectList(await _service.GetAllAsync(), "Id", "Name");
                ModelState.AddModelError("CustomError", "Something went wrong!");
                return View(dto);
            }
        }
    }
}
