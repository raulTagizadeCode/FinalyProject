using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface ITableCategoryNumberService
    {
        Task<ICollection<GetTableCategoryNumberDto>> GetAllAsync();
        Task<ICollection<GetTableCategoryNumberDto>> GetCategoryListItemsAsync();
        Task<UpdateTableCategoryNumberDto> GetByIdForUpdateAsync(int id);
        Task<TableCategoryNumber> GetByIdWithChildrenAsync(int id);
        Task<GetTableCategoryNumberDto> GetByIdAsync(int id);
        Task<TableCategoryNumber> GetByIdMasaAsync(int id);
        Task CreateAsync(CreateTableCategoryNumberDto entity);
        Task UpdateAsync(UpdateTableCategoryNumberDto entity);
        Task DeleteAsync(int id);
    }
}
