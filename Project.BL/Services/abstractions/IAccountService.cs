using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.AppUserDTOs;

namespace Project.BL.Services.abstractions
{
    public interface IAccountService
    {
        Task RegisterAsync(AppUserRegisterDto dto);
        Task LoginAsync(AppUserLogin dto);
        Task LogoutAsync();
    }
}
