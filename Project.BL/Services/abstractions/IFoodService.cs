using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IFoodService
    {
        Task<ICollection<GetFoodDto>> GetAllAsync();
        Task<GetFoodDto> GetByIdAsync(int id);
        Task<UpdateFoodDto> GetByIdForUpdateAsync(int id);
        Task<Food> GetByIdWithChildrenAsync(int id);
        Task CreateAsync(CreateFoodDto entity);
        Task UpdateAsync(UpdateFoodDto entity);
        Task DeleteAsync(int id);
    }
}
