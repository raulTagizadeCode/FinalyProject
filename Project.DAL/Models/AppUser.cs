
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project.DAL.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Rayting> Raytings { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Order> Orders { get; set; }

    }
    public class Reservation : BaseEntity
    {
        public int MasaId { get; set; }
        public Masa Masa { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int PersonCount { get; set; }
        public TimeSpan ReservationTime { get; set; } // ⏳ İstifadəçinin seçdiyi saat
        public bool confirmation { get; set; }

    }
    public class Rayting: BaseEntity
    {
        [Range(1, 5)]
        public int Score { get; set; }
        public string? Comment { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
   
}
