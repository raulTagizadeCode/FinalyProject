using Project.DAL.Models;

namespace Project.BL.DTOs.BasketItemDTOs
{
    public class OrderCreateDto
    {
        public string AppUserId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public double TotalPrice { get; set; }
        public int OrderCategoryId { get; set; }
        public string Location { get;set; }
        
    }
    public class GetOrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public string AppUserId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public double TotalPrice { get; set; }
        public string AppUserName { get; set; }
        public string AppUserEmail { get; set; }
        public string FoodImagePath { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public string FoodPrice { get; set; }
        public string OrderCategoryName { get; set; }
        public string Location { get; set; }
    }
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
