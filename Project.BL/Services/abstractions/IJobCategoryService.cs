using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.JobCategoryDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IJobCategoryService
    {
        Task<ICollection<GetJobCategoryDto>> GetAllAsync();
        Task<JobCategory> GetAllJobAsync();
        Task<ICollection<GetJobCategoryDto>> GetCategoryListItemsAsync();
        Task<UpdateJobCategoryDto> GetByIdForUpdateAsync(int id);
        Task<JobCategory> GetByIdWithChildrenAsync(int id);
        Task<GetJobCategoryDto> GetByIdAsync(int id);
        Task<Job> GetByIdJobAsync(int id);
        Task CreateAsync(CreateJobCategoryDto entity);
        Task UpdateAsync(UpdateJobCategoryDto entity);
        Task DeleteAsync(int id);
    }
}
