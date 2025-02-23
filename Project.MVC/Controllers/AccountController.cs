using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.BL.Abstractions;
using Project.BL.DTOs.AppUserDTOs;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly IAccountService _accountService;
        readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        readonly IEmailService _emailService;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IMapper mapper, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError", "Something went wrong");
                return View(appUserRegisterDto);
            }
            AppUser user = _mapper.Map<AppUser>(appUserRegisterDto);

            try
            {
                await _accountService.RegisterAsync(appUserRegisterDto);
                string userToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string? url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = userToken },Request.Scheme);
              //  _emailService.SendWelcome(user.Email, url);
                return RedirectToAction("Login", "Account");
            }
            catch(Exception e) 
            {
                ModelState.AddModelError("CustomError", e.Message);
                return View(appUserRegisterDto);
            }

        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUserLogin appUserLogin)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("CustomError", "Something went wrong");
                return View(appUserLogin);
            }
            
            try
            {
                await _accountService.LoginAsync(appUserLogin);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CustomError", e.Message);
                return View();
            }

        }
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _accountService.LogoutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CustomError", e.Message);
                return View();
            }

        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Problem");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Confirmed email");
            }
            return BadRequest("Problem occured");
        }
    }
}
