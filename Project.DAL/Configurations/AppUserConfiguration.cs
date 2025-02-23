using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.Models;

namespace Project.DAL.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(255);
        }

    }
    public class RaytingConfiguration : IEntityTypeConfiguration<Rayting>
    {
        public void Configure(EntityTypeBuilder<Rayting> builder)
        {

            builder.Property(x => x.Score).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(255);
        }
    }
    public class RezervationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
        }
    }
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.AppUser).WithMany(x => x.Orders).HasForeignKey(x => x.AppUserId);
        }
    }
}
