using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.BasketItemDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.DTOs.MasaDTOs;
using Project.BL.DTOs.RaytingDTOs;
using Project.DAL.Models;

namespace Project.BL.Services.abstractions
{
    public interface IOrderService
    {
        Task<ICollection<GetOrderDto>> GetAllAsync();
        Task CreateOrderAsync(Order dto);
        Task UpdateAsync(UpdateOrderDto entity);
        Task DeleteAsync(int id);
        Task<Order> GetByIdAsync(int id);
        Task<UpdateOrderDto> GetByIdForUpdateAsync(int id);
    }
}
