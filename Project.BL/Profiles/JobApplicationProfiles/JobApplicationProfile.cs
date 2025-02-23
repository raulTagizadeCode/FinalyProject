using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.DAL.Models;

namespace Project.BL.Profiles.JobApplicationProfiles
{
    public class JobApplicationProfile:Profile
    {
        public JobApplicationProfile()
        {
            CreateMap<GetJobApplicationDto,JobApplication>().ReverseMap()
                .ForMember(dest => dest.JobName, options => options.MapFrom(src => src.Job.Name)); ;
            CreateMap<CreateJobApplicationDto,JobApplication>().ReverseMap();
            CreateMap<UpdateJobApplicationDto, JobApplication>().ReverseMap();
        }
    }
}
