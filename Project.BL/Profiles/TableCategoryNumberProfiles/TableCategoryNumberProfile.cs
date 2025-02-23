using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.TableCategoryNumberProfiles
{
    public class TableCategoryNumberProfile:Profile
    {
        public TableCategoryNumberProfile()
        {
            CreateMap<GetTableCategoryNumberDto,TableCategoryNumber>().ReverseMap();
            CreateMap<CreateTableCategoryNumberDto, TableCategoryNumber>().ReverseMap();
            CreateMap<UpdateTableCategoryNumberDto, TableCategoryNumber>().ReverseMap();
        }
    }
}
