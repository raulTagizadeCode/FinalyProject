using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Models;

namespace Project.BL.DTOs.BasketItemDTOs
{

    public class BasketItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FoodId { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }
    }
    public class BasketDto
    {
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public double TotalPrice
        {
            get
            {
                return Items.Any() ? Items.Sum(item => (item.Price * item.Quantity)) + 5 : 0;

            }
        }
    }
}
