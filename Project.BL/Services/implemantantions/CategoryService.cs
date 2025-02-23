using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class CategoryService : ICategoryService
    {
        readonly IRepository<Category> _repository;
        readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCategoryDto entity)
        {
            var mapper = _mapper.Map<Category>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetCategoryDto>>(res);
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetCategoryDto>(res);
        }



        public async Task<UpdateCategoryDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateCategoryDto>(res);
        }
        public async Task<ICollection<GetCategoryDto>> GetCategoryListItemsAsync() => _mapper.Map<ICollection<GetCategoryDto>>(await _repository.GetAllAsync());
        public async Task<Category> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "Foods") ?? throw new BaseException();
        public async Task UpdateAsync(UpdateCategoryDto entity)
        {
            var id = entity.Id;
            var res = await GetByIdAsync(id);
            var mapper = _mapper.Map<Category>(entity);
            mapper.UpdateAt = DateTime.Now;
            await _repository.UpdateAsync(mapper);
        }

        public async Task<Category> GetByIdFoodAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "Foods");
            if (res == null)
            {
                throw new Exception("Entity not Fount");
            }
            var mapper = _mapper.Map<Category>(res);
            return mapper;
        }
    }
}
