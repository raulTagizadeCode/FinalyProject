using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.FoodDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Utilites;
using Project.DAL.Enums;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class JobApplicationService : IJobApplicationService
    {
        readonly IRepository<JobApplication> _repository;
        readonly IRepository<Job> _jobRepository;
        readonly IMapper _mapper;

        public JobApplicationService(IRepository<Job> jobRepository, IRepository<JobApplication> repository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public JobApplicationService(IRepository<JobApplication> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateJobApplicationDto entity)
        {
            if (await _jobRepository.GetByIdAsync(entity.JobId) is null) throw new BaseException("Category not found!");

            var mapper = _mapper.Map<JobApplication>(entity);
            mapper.CreatedAt = DateTime.Now;

            mapper.CvUrl = await entity.Cv.SaveAsync("cvs");
            mapper.ImageUrl = await entity.Image.SaveAsync("personals");
            await _repository.CreateAsync(mapper);
        }

        public async Task DeleteAsync(int id)
        {
            var place = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(place);
            File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "cvs", place.CvUrl));
        }

        public async Task<ICollection<GetJobApplicationDto>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync("Job");

            // JobStatus dəyəri Accepted olanları filtr edirik
            var filteredRes = res.Where(x => x.JobStatus == JobStatus.Accepted).ToList();

            return _mapper.Map<ICollection<GetJobApplicationDto>>(filteredRes);
        }

        public async Task<GetJobApplicationDto> GetByIdAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetJobApplicationDto>(res);
        }

        public async Task<UpdateJobApplicationDto> GetByIdForUpdateAsync(int id)
        {
            var res = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateJobApplicationDto>(res);
        }

        public async Task<JobApplication> GetByIdWithChildrenAsync(int id) => await _repository.GetByIdAsync(id) ?? throw new BaseException();


        public async Task UpdateAsync(UpdateJobApplicationDto dto)
        {
            //if (await _jobRepository.GetByIdAsync(dto.JobId) is null) throw new BaseException("Category not found!");

            var oldPlace = await GetByIdAsync(dto.Id);
            var place = _mapper.Map<JobApplication>(dto);

            //   place.CvUrl = dto.Cv is not null ? await dto.Cv.SaveAsync("cvs") : oldPlace.CvUrl;

            await _repository.UpdateAsync(place);

            //  if (dto.Cv is not null) File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "cvs", oldPlace.CvUrl));
        }

        public async Task<ICollection<GetJobApplicationDto>> GetAllAspazAsync()
        {
            var res = await _repository.GetAllAsync("Job");

            // JobStatus dəyəri Accepted olanları filtr edirik
            var filteredRes = res.Where(x => x.JobStatus == JobStatus.Accepted && x.JobId == 1).ToList();
            return _mapper.Map<ICollection<GetJobApplicationDto>>(filteredRes);
        }
    }
}
