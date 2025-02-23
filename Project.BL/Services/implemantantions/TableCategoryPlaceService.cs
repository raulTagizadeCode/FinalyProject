using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.BL.DTOs.TableCategoryPlaceDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class TableCategoryPlaceService : ITableCategoryPlaceService
    {
        readonly IRepository<TableCategoryPlace> _repository;

        readonly IMapper _mapper;
        public TableCategoryPlaceService(IMapper mapper, IRepository<TableCategoryPlace> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateAsync(CreateTableCategoryPlaceDto entity)
        {
            var mapper = _mapper.Map<TableCategoryPlace>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetTableCategoryPlaceDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetTableCategoryPlaceDto>>(res);
        }

        public async Task<GetTableCategoryPlaceDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetTableCategoryPlaceDto>(res);
        }

        public async Task<UpdateTableCategoryPlaceDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateTableCategoryPlaceDto>(res);
        }

        public async Task<TableCategoryPlace> GetByIdMasaAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "Tables");
            if (res == null)
            {
                throw new Exception("Entity not Fount");
            }
            var mapper = _mapper.Map<TableCategoryPlace>(res);
            return mapper;
        }

        public async Task<TableCategoryPlace> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "Tables") ?? throw new BaseException();

        public async Task<ICollection<GetTableCategoryPlaceDto>> GetCategoryListItemsAsync() => _mapper.Map<ICollection<GetTableCategoryPlaceDto>>(await _repository.GetAllAsync());

        public async Task UpdateAsync(UpdateTableCategoryPlaceDto entity)
        {
            var id = entity.Id;
            var res = await GetByIdAsync(id);
            var mapper = _mapper.Map<TableCategoryPlace>(entity);
            mapper.UpdateAt = DateTime.Now;
            await _repository.UpdateAsync(mapper);
        }
    }
}

