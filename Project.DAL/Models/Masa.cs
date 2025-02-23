using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Models
{
    public class Masa:BaseEntity
    {
        public int TableNumber { get; set; }
        public int TableCategoryNumberId { get; set; }
        public TableCategoryNumber TableCategoryNumber { get; set; }
        public int TableCategoryPlaceId { get; set; }
        public TableCategoryPlace TableCategoryPlace { get; set; }
        public bool IsActive { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
    public class TableCategoryNumber: BaseEntity
    {
        public string Name { get; set; }
        public List<Masa> Tables { get; set; }
    }
    public class TableCategoryPlace : BaseEntity
    {
        public string Name { get; set; }
        public List<Masa> Tables { get; set; }
    }
    public class Setting
    {
        public int Id { get; set; }  // Primary Key
        public string Key { get; set; } = string.Empty; // Məsələn: "LastTableReset"
        public string Value { get; set; } = string.Empty; // Məsələn: "2024-02-05"
    }


}
