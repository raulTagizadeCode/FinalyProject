using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Enums;
using Project.DAL.Models;

namespace Project.BL.DTOs.JobDTOs
{
    public class GetJobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public double Salary { get; set; }
        public string JobCategoryName { get; set; }
        public DateTime CreateAt { get; set; }
    }

}
