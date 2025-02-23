using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.MVC.Controllers
{
    public class RaytingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        readonly IRepository<Rayting> _repository;
        public RaytingController(AppDbContext context, UserManager<AppUser> userManager, IRepository<Rayting> repository)
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;
        }
        public IActionResult SubmitRating()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitRating(int score, string? comment)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Message", "Rayting");

            // İstifadəçinin artıq reyting verib-vermədiyini yoxlayırıq
            var existingRating = await _context.Raytings.FirstOrDefaultAsync(r => r.AppUserId == user.Id);
            if (existingRating != null)
            {
                return BadRequest("Siz artıq reyting vermisiniz.");
            }

            var rating = new Rayting
            {
                Score = score,
                Comment = comment,
                AppUserId = user.Id
            };

            await _context.Raytings.AddAsync(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Message()
        {
            return View();
        }
    }
}
