using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Models;

namespace Project.BL.DTOs.RaytingDTOs
{
    public class GetRaytingDto
    {
        public int Score { get; set; }
        public string? Comment { get; set; }
        public string AppUserId { get; set; }
        public string AppUserName1 { get; set; }
        public string AppUserName { get; set; }
        public string AppUserSurname { get; set; }
    }
    public class CreateRaytingDto
    {
        public int Score { get; set; }
        public string? Comment { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
