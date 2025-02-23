using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.BL.DTOs.MasaDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.BL.Services.implemantantions
{
    public class ReservationService : IReservationService
    {
        readonly AppDbContext _context;
        readonly IRepository<Reservation> _reservationRepository;
        readonly IRepository<Masa> _repository;
        public ReservationService(AppDbContext context, IRepository<Masa> repository, IRepository<Reservation> reservationRepository)
        {
            _context = context;
            _repository = repository;
            _reservationRepository = reservationRepository;
        }

        public Task CreateAsync(Reservation entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Reservation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetMasaDto>> GetAvailableTablesAsync(int categoryId, int placeId)
        {
            var res = (await _repository.GetAllAsync())
                .Where(m => m.TableCategoryNumberId == categoryId &&
                            m.TableCategoryPlaceId == placeId &&
                            m.IsActive)  // Yalnız aktiv masalar
                .Select(m => new GetMasaDto  // Dataları DTO-ya çeviririk
                {
                    Id = m.Id,
                    TableNumber = m.TableNumber,
                    TableCategoryNumberId = m.TableCategoryNumberId,
                    TableCategoryPlaceId = m.TableCategoryPlaceId,
                    IsActive = m.IsActive
                })
                .ToList();

            return res;
        }

        public Task<Reservation> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation> GetByIdForUpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> GetByIdWithChildrenAsync(int id) => await _reservationRepository.GetByIdAsync(id) ?? throw new BaseException();


        public async Task<bool> ReserveTableAsync(int tableId, string userId, TimeSpan reservationTime, int personCount, bool confirmation)
        {
            var today = DateTime.UtcNow.Date;

            var existingReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.AppUserId == userId);

            var table = await _context.Masas.FindAsync(tableId);
            if (table == null || !table.IsActive)
                return false;

            table.IsActive = false;
            _context.Masas.Update(table);

            _context.Reservations.Add(new Reservation
            {
                MasaId = tableId,
                AppUserId = userId,
                CreatedAt = DateTime.Now,  // 📌 Bu günün tarixi
                ReservationTime = reservationTime, // 📌 İstifadəçinin seçdiyi saat (TimeSpan)
                PersonCount = personCount,
                confirmation = confirmation

            });

            await _context.SaveChangesAsync();
            return true;
        }

        public Task UpdateAsync(Reservation entity)
        {
            throw new NotImplementedException();
        }
    }
}
