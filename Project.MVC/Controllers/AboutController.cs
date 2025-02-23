using Microsoft.AspNetCore.Mvc;
using Project.BL.Services.abstractions;

namespace Project.MVC.Controllers
{
    public class AboutController : Controller
    {
        readonly IJobApplicationService _jobApplicationService;

        public AboutController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _jobApplicationService.GetAllAspazAsync();
            return View(res);
        }
    }
}
