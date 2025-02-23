using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.BL.DTOs.BasketItemDTOs;
using Project.BL.DTOs.JobApplicationDTOs;
using Project.BL.Exceptions;
using Project.BL.Services.abstractions;
using Project.BL.Services.implemantantions;
using Project.DAL.Contexts;
using Project.DAL.Enums;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;

namespace Project.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        readonly IOrderService _orderService;
        readonly AppDbContext _context;
        readonly IRepository<Order> _repository;
        public OrderController(IOrderService orderService, AppDbContext context, IRepository<Order> repository)
        {
            _orderService = orderService;
            _context = context;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _orderService.GetAllAsync();
            return View(res);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
        public async Task<IActionResult> Status(Order order)
        {
            var masaa = await _repository.GetByIdAsync(order.Id);
            if (masaa == null)
                return NotFound("Order tapılmadı.");
            masaa.Status = OrderStatus.Sent;
            await _repository.UpdateAsync(masaa);
            return RedirectToAction("Index", "Order");
        }
    }
}