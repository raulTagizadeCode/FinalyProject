using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.JobCategoryDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class JobCategoryService : IJobCategoryService
    {
        readonly IRepository<JobCategory> _repository;
        readonly IMapper _mapper;
        public JobCategoryService(IRepository<JobCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateJobCategoryDto entity)
        {
            var mapper = _mapper.Map<JobCategory>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetJobCategoryDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<GetJobCategoryDto>>(res);
        }

        public async Task<GetJobCategoryDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetJobCategoryDto>(res);
        }

        public async Task<Job> GetByIdJobAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "Foods");
            if (res == null)
            {
                throw new Exception("Entity not Fount");
            }
            var mapper = _mapper.Map<Job>(res);
            return mapper;
        }

        public async Task<UpdateJobCategoryDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateJobCategoryDto>(res);
        }

        public async Task<JobCategory> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "Jobs") ?? throw new BaseException();


        public async Task<ICollection<GetJobCategoryDto>> GetCategoryListItemsAsync() => _mapper.Map<ICollection<GetJobCategoryDto>>(await _repository.GetAllAsync());


        public async Task UpdateAsync(UpdateJobCategoryDto entity)
        {
            var id = entity.Id;
            var res = await GetByIdAsync(id);
            var mapper = _mapper.Map<JobCategory>(entity);
            mapper.UpdateAt = DateTime.Now;
            await _repository.UpdateAsync(mapper);
        }

        public async Task<JobCategory> GetAllJobAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<JobCategory>(res);
        }
    }
}
