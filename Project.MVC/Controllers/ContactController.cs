using Microsoft.AspNetCore.Mvc;

namespace Project.MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
