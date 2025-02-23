using FluentValidation;

namespace Project.BL.DTOs.JobDTOs
{
    public class UpdateJobDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public double Salary { get; set; }
        public int JobCategoryId { get; set; }
    }
    public class UpdateJobValidator : AbstractValidator<UpdateJobDto>
    {
        public UpdateJobValidator()
        {
            RuleFor(job => job.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(job => job.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");

            RuleFor(job => job.Requirements)
                .NotEmpty().WithMessage("Requirements are required.")
                .Length(10, 500).WithMessage("Requirements must be between 10 and 500 characters.");

            RuleFor(job => job.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");
        }
    }

}
