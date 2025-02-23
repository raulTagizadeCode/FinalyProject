using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.AppUserDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.AppUserProfiles
{
    public  class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUserRegisterDto, AppUser>().ReverseMap();
            CreateMap<AppUserLogin, AppUser>().ReverseMap();
        }
    }
}
