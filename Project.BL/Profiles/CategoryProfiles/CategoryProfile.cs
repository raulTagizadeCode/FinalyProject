using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.CategoryDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.CategoryProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<GetCategoryDto, Category>().ReverseMap();
        }
    }
}
