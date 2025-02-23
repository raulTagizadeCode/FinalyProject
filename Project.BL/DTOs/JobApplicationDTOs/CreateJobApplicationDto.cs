using FluentValidation;
using Microsoft.AspNetCore.Http;
using Project.BL.Utilites;
using Project.DAL.Enums;

namespace Project.BL.DTOs.JobApplicationDTOs
{
    public class CreateJobApplicationDto
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public JobStatus JobStatus { get; set; }
        public int UserAge { get; set; }
        public IFormFile Cv { get; set; }
        public IFormFile Image { get; set; }
        public int JobId { get; set; }
    }
    public class CreateJobApplicationDtoValidation : AbstractValidator<CreateJobApplicationDto>
    {
        public CreateJobApplicationDtoValidation()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .Length(2, 50).WithMessage("UserName must be between 2 and 50 characters.");

            RuleFor(user => user.UserSurname)
                .NotEmpty().WithMessage("UserSurname is required.")
                .Length(2, 50).WithMessage("UserSurname must be between 2 and 50 characters.");

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage("UserEmail is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.JobStatus)
                .IsInEnum().WithMessage("Invalid JobStatus value.");

            RuleFor(user => user.UserAge)
                .InclusiveBetween(18, 65).WithMessage("UserAge must be between 18 and 65.");

            RuleFor(x => x.Image)
              .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage("Cv cannot be null!")
              .Must(x => x.Length <= 3 * 1024 * 1024).WithMessage("File size must be less than 3 MB!")
              .Must(x => x.CheckType("image")).WithMessage("Cv must be Image!"); ;


            //RuleFor(x => x.Cv)
            //  .Cascade(CascadeMode.Stop)
            //  .NotNull().WithMessage("Cv cannot be null!")
            //  .Must(x => x.Length <= 10 * 1024 * 1024).WithMessage("File size must be less than 10 MB!")
            //  .Must(x => x.CheckType("image")).WithMessage("Cv must be Image!"); ;

        }

    }
    }
