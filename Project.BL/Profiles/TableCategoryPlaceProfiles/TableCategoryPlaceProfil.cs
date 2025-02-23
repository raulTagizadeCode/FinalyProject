using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.TableCategoryPlaceDTOs;
using Project.BL.Profiles.TableCategoryNumberProfiles;
using Project.DAL.Models;

namespace Project.BL.Profiles.TableCategoryPlaceProfiles
{
    public class TableCategoryPlaceProfil:Profile
    {
        public TableCategoryPlaceProfil()
        {
            CreateMap<GetTableCategoryPlaceDto, TableCategoryPlace>().ReverseMap();
            CreateMap<CreateTableCategoryPlaceDto, TableCategoryPlace>().ReverseMap();
            CreateMap<UpdateTableCategoryPlaceDto, TableCategoryPlace>().ReverseMap();
        }
    }
}
