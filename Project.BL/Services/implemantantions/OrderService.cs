using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.BasketItemDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.DTOs.MasaDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class OrderService : IOrderService
    {
        readonly IMapper _mapper;
        readonly AppDbContext _appDbContext;
        readonly IRepository<Order> _repository;
        public OrderService(IMapper mapper, AppDbContext appDbContext, IRepository<Order> repository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _repository = repository;
        }

        public async Task CreateOrderAsync(Order dto)
        {
            await _repository.CreateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetOrderDto>> GetAllAsync()
        {
            //var res =  await _appDbContext.Orders.Where(x=>x.Status == OrderStatus.wasNoted).ToListAsync();
            var res = await _repository.GetAllAsync("AppUser");
            return _mapper.Map<ICollection<GetOrderDto>>(res);



        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id, "AppUser");
        }

        public async Task UpdateAsync(UpdateOrderDto entity)
        {
            var oldPlace = await GetByIdAsync(entity.Id);
            var place = _mapper.Map<Order>(entity);

            //   place.CvUrl = dto.Cv is not null ? await dto.Cv.SaveAsync("cvs") : oldPlace.CvUrl;

            await _repository.UpdateAsync(place);

        }
        public async Task<UpdateOrderDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateOrderDto>(res);
        }
    }
}
