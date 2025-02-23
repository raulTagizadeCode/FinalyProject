using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.TableCategoryNumberDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class TableCategoryNumberService : ITableCategoryNumberService
    {
        readonly IRepository<TableCategoryNumber> _repository;

        readonly IMapper _mapper;
        public TableCategoryNumberService(IMapper mapper, IRepository<TableCategoryNumber> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateAsync(CreateTableCategoryNumberDto entity)
        {
            var mapper = _mapper.Map<TableCategoryNumber>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetTableCategoryNumberDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetTableCategoryNumberDto>>(res);
        }

        public async Task<GetTableCategoryNumberDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetTableCategoryNumberDto>(res);
        }

        public async Task<UpdateTableCategoryNumberDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateTableCategoryNumberDto>(res);
        }

        public async Task<TableCategoryNumber> GetByIdMasaAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "Tables");
            if (res == null)
            {
                throw new Exception("Entity not Fount");
            }
            var mapper = _mapper.Map<TableCategoryNumber>(res);
            return mapper;
        }

        public async Task<TableCategoryNumber> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "Tables") ?? throw new BaseException();

        public async Task<ICollection<GetTableCategoryNumberDto>> GetCategoryListItemsAsync() => _mapper.Map<ICollection<GetTableCategoryNumberDto>>(await _repository.GetAllAsync());

        public async Task UpdateAsync(UpdateTableCategoryNumberDto entity)
        {
            var id = entity.Id;
            var res = await GetByIdAsync(id);
            var mapper = _mapper.Map<TableCategoryNumber>(entity);
            mapper.UpdateAt = DateTime.Now;
            await _repository.UpdateAsync(mapper);
        }
    }
}
