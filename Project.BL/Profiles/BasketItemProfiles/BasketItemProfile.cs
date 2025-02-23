using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.BasketItemDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.BasketItemProfiles
{
    public class BasketItemProfile:Profile
    {
        public BasketItemProfile()
        {
            CreateMap<BasketItemDto,BasketItem>().ReverseMap();
            CreateMap<OrderCreateDto,Order>().ReverseMap();
            CreateMap<GetOrderDto, Order>().ReverseMap()
                .ForMember(dest => dest.AppUserName, options => options.MapFrom(src => src.AppUser.FirstName));
            CreateMap<GetOrderDto, BasketItem>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();
            CreateMap<OrderCreateDto, BasketItem>().ReverseMap();
        }

    }
}
