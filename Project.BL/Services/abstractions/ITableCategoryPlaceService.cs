using Project.BL.DTOs.TableCategoryPlaceDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface ITableCategoryPlaceService
    {
        Task<ICollection<GetTableCategoryPlaceDto>> GetAllAsync();
        Task<ICollection<GetTableCategoryPlaceDto>> GetCategoryListItemsAsync();
        Task<UpdateTableCategoryPlaceDto> GetByIdForUpdateAsync(int id);
        Task<TableCategoryPlace> GetByIdWithChildrenAsync(int id);
        Task<GetTableCategoryPlaceDto> GetByIdAsync(int id);
        Task<TableCategoryPlace> GetByIdMasaAsync(int id);
        Task CreateAsync(CreateTableCategoryPlaceDto entity);
        Task UpdateAsync(UpdateTableCategoryPlaceDto entity);
        Task DeleteAsync(int id);
    }
}
