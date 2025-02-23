using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobDTOs;
using Project.BL.DTOs.RaytingDTOs;

namespace Project.BL.Services.abstractions
{
    public interface IRaytingService
    {
        Task<ICollection<GetRaytingDto>> GetAllAsync();
    }
}
