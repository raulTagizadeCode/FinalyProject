using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Project.BL.Abstractions;
using Project.BL.DTOs.AppUserDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Utilites;
using Project.DAL.Enums;
using Project.DAL.Models;

namespace Project.BL.Services.implemantantions
{
    public class AccountService : IAccountService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IEmailService _emailservice;
        readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IEmailService emailservice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailservice = emailservice;
        }
        public async Task LoginAsync(AppUserLogin loginDTO)
        {
            AppUser? userDB = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (userDB is null)
            {
                throw new Exception("Credentials are not correct.");
            }
            //if (!userDB.EmailConfirmed)
            //{
            //    throw new Exception("please confirm your email");
            //}
            bool isAllowed = await _userManager.CheckPasswordAsync(userDB, loginDTO.Password);
            if (!isAllowed)
            {
                throw new Exception("Credentials are not correct.");
            }
            await _signInManager.SignInAsync(userDB, true);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterAsync(AppUserRegisterDto appUserRegisterDto)
        {
            var existingUser = await _userManager.FindByNameAsync(appUserRegisterDto.UserName);
            if (existingUser != null)
            {
                // Username artıq mövcuddur
                throw new Exception("Bu username artıq istifadə olunub.");
            }
            AppUser user = _mapper.Map<AppUser>(appUserRegisterDto);

            var res = await _userManager.CreateAsync(user, appUserRegisterDto.Password);

            var result = await _userManager.AddToRoleAsync(user, RoleEnum.User.ToString());
            _emailservice.SendWelcome(user.Email);

        }
    }
}
