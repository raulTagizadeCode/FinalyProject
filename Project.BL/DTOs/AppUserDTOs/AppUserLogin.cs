using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.DTOs.AppUserDTOs
{
    public class AppUserLogin
    {
        [Required]
        [Display(Prompt = "UserName ")]
        public string UserName { get; set; }
        [Required]
        [Display(Prompt = "Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
