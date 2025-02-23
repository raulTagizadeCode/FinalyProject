using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.DAL.Models;

namespace Project.DAL.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Masa>
    {
        public void Configure(EntityTypeBuilder<Masa> builder)
        {
            builder.HasOne(builder => builder.TableCategoryNumber)
                .WithMany(builder => builder.Tables)
                .HasForeignKey(builder => builder.TableCategoryNumberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(builder => builder.TableCategoryPlace)
                .WithMany(builder => builder.Tables)
                .HasForeignKey(builder => builder.TableCategoryPlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }


}
