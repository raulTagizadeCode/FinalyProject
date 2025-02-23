using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.BL.DTOs.CategoryDTOs;
using Project.BL.DTOs.JobDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Enums;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;
using Project.DAL.Repository.implemantantions;

namespace Project.BL.Services.implemantantions
{
    public class JobService : IJobService
    {
        readonly IRepository<Job> _repository;
        readonly IMapper _mapper;
        readonly IRepository<JobCategory> _jobCategoryRepository;
        public JobService(IRepository<Job> jobRepository, IMapper mapper, IRepository<JobCategory> jobCategoryRepository)
        {
            _repository = jobRepository;
            _mapper = mapper;
            _jobCategoryRepository = jobCategoryRepository;
        }

        public async Task CreateAsync(CreateJobDto entity)
        {
            if (await _jobCategoryRepository.GetByIdAsync(entity.JobCategoryId) is null) throw new BaseException("Category not found!");

            var mapper = _mapper.Map<Job>(entity);
            mapper.CreatedAt = DateTime.Now;
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(res);
        }

        public async Task<ICollection<GetJobDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync("JobCategory");
            return _mapper.Map<ICollection<GetJobDto>>(res);
        }

        public async Task<GetJobDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetJobDto>(res);
        }

        public async Task<UpdateJobDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateJobDto>(res);
        }

        public async Task<JobUpdateStatusDto> GetByIdForUpdateStatusAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<JobUpdateStatusDto>(res);
        }

        public async Task<Job> GetByIdJobApplicationAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id, "JobApplications");
            if (res == null)
            {
                throw new Exception("Entity not Fount");
            }
            var mapper = _mapper.Map<Job>(res);
            return mapper;
        }

        public async Task<Job> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id, includes: "JobApplications") ?? throw new BaseException();


        public async Task<ICollection<GetJobDto>> GetJobListItemsAsync() => _mapper.Map<ICollection<GetJobDto>>(await _repository.GetAllAsync());


        public async Task UpdateAsync(UpdateJobDto entity)
        {
            if (await _jobCategoryRepository.GetByIdAsync(entity.JobCategoryId) is null) throw new BaseException("Category not found!");

            var id = entity.Id;
            var res = await GetByIdAsync(id);
            var mapper = _mapper.Map<Job>(entity);
            mapper.UpdateAt = DateTime.Now;
            await _repository.UpdateAsync(mapper);
        }
    }
}
