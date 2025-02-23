using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Models
{
    public class Job : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public double Salary { get; set; }
        public int JobCategoryId { get; set; }
        public JobCategory JobCategory { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
}
