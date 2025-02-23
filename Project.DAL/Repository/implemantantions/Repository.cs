using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.DAL.Repository.implemantantions
{
    public class Repository<T>(AppDbContext _context) : IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table => _context.Set<T>();

        public async Task CreateAsync(T entity)
        {
             await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = Table;
            if (includes.Length > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = Table;
            if (includes.Length > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
          Table.Update(entity);
          await _context.SaveChangesAsync();
        }
    }
}
