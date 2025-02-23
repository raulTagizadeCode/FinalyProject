using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.DTOs.TableCategoryPlaceDTOs
{
    public class GetTableCategoryPlaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateTableCategoryPlaceDto
    {
        public string Name { get; set; }
    }
    public class UpdateTableCategoryPlaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
