using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Models;

namespace Project.BL.DTOs.MasaDTOs
{
    public class GetMasaDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int TableCategoryNumberId { get; set; }
        public string TableCategoryNumberName { get; set; }
        public int TableCategoryPlaceId { get; set; }
        public string TableCategoryPlaceName { get; set; }
        public bool IsActive { get; set; }

    }
    public class CreateMasaDto
    {
        public int TableNumber { get; set; }
        public int TableCategoryNumberId { get; set; }
        public int TableCategoryPlaceId { get; set; }
        public bool IsActive { get; set; }

    }
    public class UpdateMasaDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int TableCategoryNumberId { get; set; }
        public int TableCategoryPlaceId { get; set; }
        public bool IsActive { get; set; }

    }

}
