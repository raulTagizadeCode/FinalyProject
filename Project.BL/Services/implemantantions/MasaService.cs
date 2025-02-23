using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.JobDTOs;
using Project.BL.DTOs.MasaDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;
using Project.DAL.Repository.implemantantions;

namespace Project.BL.Services.implemantantions
{
    public class MasaService : IMasaService
    {
        readonly IRepository<Masa> _masarepository;
        readonly IMapper _mapper;
        readonly IRepository<TableCategoryNumber> _numberrepo;
        readonly IRepository<TableCategoryPlace> _placerepo;
        readonly AppDbContext _context;
        public MasaService(IRepository<Masa> masarepository, IMapper mapper, IRepository<TableCategoryNumber> numberrepo, IRepository<TableCategoryPlace> placerepo, AppDbContext context)
        {
            _masarepository = masarepository;
            _mapper = mapper;
            _numberrepo = numberrepo;
            _placerepo = placerepo;
            _context = context;
        }

        public async Task ActiveAsync()
        {
            var res = _context.Masas.Where(x => x.IsActive == false).ToList();
            foreach (var item in res)
            {
                item.IsActive = true;
                await _masarepository.UpdateAsync(item);
            }

        }

        public async Task CreateAsync(CreateMasaDto entity)
        {
            if (await _numberrepo.GetByIdAsync(entity.TableCategoryNumberId) is null) throw new BaseException("Category not found!");
            if (await _placerepo.GetByIdAsync(entity.TableCategoryPlaceId) is null) throw new BaseException("Category not found!");
            var mapper = _mapper.Map<Masa>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _masarepository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _masarepository.GetByIdAsync(id);
            await _masarepository.DeleteAsync(res);
        }

        public async Task<ICollection<GetMasaDto>> GetAllAsync()
        {
            var res = await _masarepository.GetAllAsync("TableCategoryNumber", "TableCategoryPlace");
            return _mapper.Map<ICollection<GetMasaDto>>(res);
        }

        public async Task<GetMasaDto> GetByIdAsync(int id)
        {
            var res = await _masarepository.GetByIdAsync(id);
            return _mapper.Map<GetMasaDto>(res);
        }

        public async Task<UpdateMasaDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _masarepository.GetByIdAsync(id);
            return _mapper.Map<UpdateMasaDto>(res);
        }

        public async Task<Masa> GetByIdWithChildrenAsync(int id) => await _masarepository.GetByIdAsync(id, includes: "Reservations") ?? throw new BaseException();


        public async Task UpdateAsync(UpdateMasaDto entity)
        {
            if (await _numberrepo.GetByIdAsync(entity.TableCategoryNumberId) is null) throw new BaseException("Category not found!");
            if (await _placerepo.GetByIdAsync(entity.TableCategoryPlaceId) is null) throw new BaseException("Category not found!");
            var mapper = _mapper.Map<Masa>(entity);
            var id = entity.Id;
            var res = await GetByIdAsync(id);
            mapper.UpdateAt = DateTime.Now;
            await _masarepository.UpdateAsync(mapper);
        }
    }
}
