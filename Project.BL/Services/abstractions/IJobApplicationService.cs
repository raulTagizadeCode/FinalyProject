using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IJobApplicationService
    {
        Task<ICollection<GetJobApplicationDto>> GetAllAsync();
        Task<ICollection<GetJobApplicationDto>> GetAllAspazAsync();
        Task<GetJobApplicationDto> GetByIdAsync(int id);
        Task<UpdateJobApplicationDto> GetByIdForUpdateAsync(int id);
        Task<JobApplication> GetByIdWithChildrenAsync(int id);
        Task CreateAsync(CreateJobApplicationDto entity);
        Task UpdateAsync(UpdateJobApplicationDto entity);
        Task DeleteAsync(int id);
    }
}
