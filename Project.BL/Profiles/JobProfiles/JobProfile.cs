using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.JobDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.JobProfile;

public class JobProfile:Profile
{
    public JobProfile()
    {
        CreateMap<GetJobDto,Job>().ReverseMap()
            .ForMember(dest => dest.JobCategoryName, options => options.MapFrom(src => src.JobCategory.Name)); 
        CreateMap<UpdateJobDto,Job>().ReverseMap();
        CreateMap<CreateJobDto, Job>().ReverseMap();
        CreateMap<JobUpdateStatusDto, Job>().ReverseMap();
    }
}
