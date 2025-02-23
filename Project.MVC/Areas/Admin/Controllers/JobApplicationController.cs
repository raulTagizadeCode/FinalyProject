using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.Abstractions;
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
    public class JobApplicationController : Controller
    {
        readonly IJobApplicationService _jobApplicationService;
        readonly IJobService _jobService;
        readonly AppDbContext _context;
        readonly IEmailService _emailService;
        public JobApplicationController(IJobApplicationService jobApplicationService, IJobService jobService, AppDbContext context, IEmailService emailService)
        {
            _jobApplicationService = jobApplicationService;
            _jobService = jobService;
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {

            try
            {
                ViewData["Jobs"] = new SelectList(await _jobService.GetAllAsync(), "Id", "Name");
                return View(await _jobApplicationService.GetByIdForUpdateAsync(id));

            }
            catch (BaseException ex)
            {
                ViewData["Jobs"] = new SelectList(await _jobService.GetAllAsync(), "Id", "Name");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                ViewData["Jobs"] = new SelectList(await _jobService.GetAllAsync(), "Id", "Name");

                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateJobApplicationDto dto)
        {
            int id = dto.Id;
            var res = await _jobApplicationService.GetByIdAsync(id);
            var application = _context.JobApplications
                .AsNoTracking() // Entity-in izlənməsini dayandırırıq
                .FirstOrDefault(a => a.Id == dto.Id);

            if (application == null) return NotFound();

            // Yalnız Status sahəsini yenilə
            application.JobStatus = dto.JobStatus;
            if (dto.JobStatus == JobStatus.Accepted)
            {
                _emailService.SendAcceptedEmail(res.UserEmail);   
            }
            else if (dto.JobStatus == JobStatus.Rejected)
            {
                _emailService.SendRejectedEmail(res.UserEmail);
            }
            _context.JobApplications.Update(application); // Bütün obyekt yenilənir, amma yalnız Status dəyişəcək
            await _context.SaveChangesAsync();
           

            return RedirectToAction("Index","Dashboard");
        }
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                return View(await _jobApplicationService.GetByIdWithChildrenAsync(id));
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
