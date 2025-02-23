using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Utilites;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.BL.Services.implemantantions
{
    public class FoodService : IFoodService
    {
        readonly IRepository<Food> _repository;
        readonly IRepository<Category> _categoryRepository;
        readonly IMapper _mapper;
        readonly IWebHostEnvironment _webHostEnvironment;
        public FoodService(IRepository<Food> service, IMapper mapper, IRepository<Category> categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = service;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CreateAsync(CreateFoodDto entity)
        {
            if (await _categoryRepository.GetByIdAsync(entity.CategoryId) is null) throw new BaseException("Category not found!");

            var mapper = _mapper.Map<Food>(entity);
            mapper.CreatedAt = DateTime.Now;

            mapper.ImageUrl = await entity.ImageUrl.SaveAsync("foods");

            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var place = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(place);
            File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "foods", place.ImageUrl));
        }
        public async Task<Food> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "Category") ?? throw new BaseException();

        public async Task<ICollection<GetFoodDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync("Category");
            return _mapper.Map<ICollection<GetFoodDto>>(res);
        }

        public async Task<GetFoodDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "Category");
            return _mapper.Map<GetFoodDto>(res);
        }

        public async Task<UpdateFoodDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateFoodDto>(res);
        }

        public async Task UpdateAsync(UpdateFoodDto dto)
        {
            if (await _categoryRepository.GetByIdAsync(dto.CategoryId) is null) throw new BaseException("Category not found!");

            var oldPlace = await GetByIdAsync(dto.Id);
            var place = _mapper.Map<Food>(dto);

            place.ImageUrl = dto.Image is not null ? await dto.Image.SaveAsync("foods") : oldPlace.ImageUrl;

            await _repository.UpdateAsync(place);

            if (dto.Image is not null) File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "foods", oldPlace.ImageUrl));
        }
    }
}
