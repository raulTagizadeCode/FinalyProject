using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IJobService
    {
        Task<ICollection<GetJobDto>> GetAllAsync();
        Task<ICollection<GetJobDto>> GetJobListItemsAsync();
        Task<GetJobDto> GetByIdAsync(int id);
        Task<UpdateJobDto> GetByIdForUpdateAsync(int id);
        Task<JobUpdateStatusDto> GetByIdForUpdateStatusAsync(int id);
        Task<Job> GetByIdJobApplicationAsync(int id);
        Task<Job> GetByIdWithChildrenAsync(int id);
        Task CreateAsync(CreateJobDto entity);
        Task UpdateAsync(UpdateJobDto entity);
        Task DeleteAsync(int id);
    }
}
