using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Models;

namespace Project.DAL.Repository.abstractions
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        DbSet<T> Table { get; }
        Task<ICollection<T>> GetAllAsync(params string[] includes);
        Task<T> GetByIdAsync(int id,params string[] includes);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
