using Project.BL.DTOs.CategoryDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync();
        Task<ICollection<GetCategoryDto>> GetCategoryListItemsAsync();
        Task<UpdateCategoryDto> GetByIdForUpdateAsync(int id);
        Task<Category> GetByIdWithChildrenAsync(int id);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task<Category> GetByIdFoodAsync(int id );
        Task CreateAsync(CreateCategoryDto entity);
        Task UpdateAsync(UpdateCategoryDto entity);
        Task DeleteAsync(int id);
    }
}
