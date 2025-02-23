using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.DAL.Models;

namespace Project.BL.DTOs.TableCategoryNumberDTOs
{
    public class GetTableCategoryNumberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateTableCategoryNumberDto
    {
        public string Name { get; set; }
    }
    public class UpdateTableCategoryNumberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
