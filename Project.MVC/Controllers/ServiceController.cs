﻿using Microsoft.AspNetCore.Mvc;

namespace Project.MVC.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
