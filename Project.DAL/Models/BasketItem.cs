using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Enums;
namespace Project.DAL.Models
{
    public class BasketItem : BaseEntity
    {
        public int FoodId { get; set; }
        public Food? Food { get; set; }
        public double Price { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public AppUser? AppUser { get; set; }
        public string AppuserId { get; set; }
    }
   
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.wasNoted;

    }
    public enum OrderStatus
    {
        wasNoted,
        Sent
    }
}