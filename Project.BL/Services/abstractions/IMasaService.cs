using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.MasaDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IMasaService
    {
        Task<ICollection<GetMasaDto>> GetAllAsync();
        Task<GetMasaDto> GetByIdAsync(int id);
        Task<UpdateMasaDto> GetByIdForUpdateAsync(int id);
        Task<Masa> GetByIdWithChildrenAsync(int id);
        Task CreateAsync(CreateMasaDto entity);
        Task UpdateAsync(UpdateMasaDto entity);
        Task DeleteAsync(int id);
        Task ActiveAsync();
    }
}
