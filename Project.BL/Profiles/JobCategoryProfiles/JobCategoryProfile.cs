using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.JobCategoryDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.JobCategoryProfiles
{
    public class JobCategoryProfile:Profile
    {
        public JobCategoryProfile()
        {
            CreateMap<GetJobCategoryDto, JobCategory>().ReverseMap();
            CreateMap<CreateJobCategoryDto, JobCategory>().ReverseMap();
            CreateMap<UpdateJobCategoryDto, JobCategory>().ReverseMap();
        }
    }
}
