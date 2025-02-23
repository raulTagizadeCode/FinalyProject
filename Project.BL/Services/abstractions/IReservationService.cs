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
    public interface IReservationService
    {
        Task<List<GetMasaDto>> GetAvailableTablesAsync(int categoryId, int placeId);
        Task<bool> ReserveTableAsync(int tableId, string userId, TimeSpan reservationTime, int PersonCount,bool confirmation);
        Task<ICollection<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> GetByIdForUpdateAsync(int id);
        Task<Reservation> GetByIdWithChildrenAsync(int id);
        Task CreateAsync(Reservation entity);
        Task UpdateAsync(Reservation entity);
        Task DeleteAsync(int id);
    }
}
