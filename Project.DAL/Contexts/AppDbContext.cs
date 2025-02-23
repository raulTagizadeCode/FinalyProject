using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Configurations;
using Project.DAL.Models;

namespace Project.DAL.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Rayting> Raytings { get; set; }
        public DbSet<TableCategoryNumber> TableCategoryNumbers { get; set; }
        public DbSet<TableCategoryPlace> TableCategoryPlaces { get; set; }
        public DbSet<Masa> Masas { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "8aad637d-42e5-4586-a140-697cd3ee8498", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "792af7b1-45f7-4238-b547-107355540960", Name = "User", NormalizedName = "USER" }
            );
            #endregion

            #region Admin
            AppUser admin = new()
            {
                Id = "be30629f-0508-461a-8fa1-0e905705e1f5",
                UserName = "admin",
                LastName = "veliyev",
                FirstName = "rauf",
                Email = "raultag@gmail.com",
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<AppUser> hasher = new();
            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            builder.Entity<AppUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = admin.Id, RoleId = "8aad637d-42e5-4586-a140-697cd3ee8498" }
            );
            #endregion

            builder.Entity<Rayting>()
           .HasOne(r => r.AppUser)
           .WithMany(u => u.Raytings)
           .HasForeignKey(r => r.AppUserId)
           .OnDelete(DeleteBehavior.Cascade); // Silme davranışı isteğe bağlıdır

            base.OnModelCreating(builder);
        }
    }
}
