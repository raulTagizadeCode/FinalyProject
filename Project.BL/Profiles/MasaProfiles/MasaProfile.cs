using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.MasaDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.MasaProfiles
{
    public class MasaProfile: Profile
    {
        public MasaProfile()
        {
            CreateMap<GetMasaDto,Masa>().ReverseMap()
                  .ForMember(dest => dest.TableCategoryPlaceName, options => options.MapFrom(src => src.TableCategoryPlace.Name))
                    .ForMember(dest => dest.TableCategoryNumberName, options => options.MapFrom(src => src.TableCategoryNumber.Name)); ;
            CreateMap<CreateMasaDto, Masa>().ReverseMap();
            CreateMap<UpdateMasaDto, Masa>().ReverseMap();
        }
    }
}
