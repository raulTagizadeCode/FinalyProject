using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.FoodDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.FoodProfiles
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<CreateFoodDto, Food>().ReverseMap();
            CreateMap<UpdateFoodDto, Food>().ReverseMap();
            CreateMap<GetFoodDto, Food>().ReverseMap()
                   .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.Name));

        }
    }
}
