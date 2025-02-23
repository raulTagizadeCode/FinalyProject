using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Project.BL.DTOs.AppUserDTOs
{
    public class AppUserRegisterDto
    {
        [Display(Prompt = "FirstName")]
        public string FirstName { get; set; }
        [Display(Prompt = "LastName")]
        public string LastName { get; set; }
        [Display(Prompt = "Email")]
        public string Email { get; set; }
        
        [Display(Prompt = "UserName")]
        public string UserName { get; set; }
        [Display(Prompt = "Password")]
        public string Password { get; set; }

    }
    public class AppUserRegisterDtoValidation : AbstractValidator<AppUserRegisterDto>
    {
        public AppUserRegisterDtoValidation()
        {
           RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Name not null is no empty")
                .MinimumLength(3).WithMessage("Name min length is 3")
                .MaximumLength(15).WithMessage("Name max length is 15 ");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Name not null is no empty")
               .MinimumLength(5).WithMessage("Name min length is 5 ")
               .MaximumLength(15).WithMessage("Name max length is 15 ");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email not null is no empty")
                .EmailAddress().WithMessage("Entity type Email");
            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("UserName not null is no empty")
               .MinimumLength(3).WithMessage("UserName min length is 3")
               .MaximumLength(15).WithMessage("UserName max length is 15 ");
           
        }
    }
}
