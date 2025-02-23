using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Enums;

namespace Project.BL.DTOs.JobDTOs
{
    public class JobUpdateStatusDto
    {
        public int Id { get; set; }
        public JobStatus JobStatus { get; set; }
    }
}
