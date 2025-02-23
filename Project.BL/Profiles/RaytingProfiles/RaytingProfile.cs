using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.RaytingDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.RaytingProfiles
{
    public class RaytingProfile:Profile
    {
        public RaytingProfile()
        {
            CreateMap<GetRaytingDto,Rayting>().ReverseMap()
                .ForMember(dest => dest.AppUserName, options => options.MapFrom(src => src.AppUser.FirstName))
                .ForMember(dest => dest.AppUserSurname, options => options.MapFrom(src => src.AppUser.LastName))
                  .ForMember(dest => dest.AppUserName1, options => options.MapFrom(src => src.AppUser.UserName)); 
            CreateMap<CreateRaytingDto, Rayting>().ReverseMap();
        }
    }
}
