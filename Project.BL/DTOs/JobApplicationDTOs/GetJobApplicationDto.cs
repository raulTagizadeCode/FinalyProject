using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Enums;
using Project.DAL.Models;

namespace Project.BL.DTOs.JobApplicationDTOs
{
    public class GetJobApplicationDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public JobStatus JobStatus { get; set; }
        public int UserAge { get; set; }
        public string CvUrl { get; set; }
        public string ImageUrl { get; set; }    
        public int JobId { get; set; }
        public string JobName { get; set; }
    }
}
