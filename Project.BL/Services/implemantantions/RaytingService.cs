using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.RaytingDTOs;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;
using Project.DAL.Repository.implemantantions;
using static System.Formats.Asn1.AsnWriter;

namespace Project.BL.Services.implemantantions
{
    public class RaytingService : IRaytingService
    {
        readonly IRepository<Rayting> _raytingRepository;
        readonly IMapper _mapper;

        public RaytingService(IRepository<Rayting> raytingRepository, IMapper mapper)
        {
            _raytingRepository = raytingRepository;
            _mapper = mapper;
        }



        public async Task<ICollection<GetRaytingDto>> GetAllAsync()
        {
            var res = await _raytingRepository.GetAllAsync("AppUser");
            return _mapper.Map<ICollection<GetRaytingDto>>(res);
        }
    }
}
