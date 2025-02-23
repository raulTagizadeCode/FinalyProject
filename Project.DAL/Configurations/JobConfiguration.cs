using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.Enums;
using Project.DAL.Models;

namespace Project.DAL.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Requirements).IsRequired();
            builder.Property(x => x.Salary).IsRequired();
            
            builder.HasOne(x => x.JobCategory).WithMany(x => x.Jobs).HasForeignKey(x => x.JobCategoryId);
        }
    }
    public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
    {
        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        }
    }
    public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasOne(x => x.Job).WithMany(x => x.JobApplications).HasForeignKey(x => x.JobId);
            builder.Property(x => x.JobStatus).IsRequired().HasDefaultValue(JobStatus.Pending);
        }
    }
}
