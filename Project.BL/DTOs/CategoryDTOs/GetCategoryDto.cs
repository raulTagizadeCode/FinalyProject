using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Models;

namespace Project.BL.DTOs.CategoryDTOs
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
    public class GetOrderCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
