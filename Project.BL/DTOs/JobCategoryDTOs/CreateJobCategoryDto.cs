using FluentValidation;

namespace Project.BL.DTOs.JobCategoryDTOs
{
    public class CreateJobCategoryDto
    {
        public string Name { get; set; }
    }
    public class CreateJobCategoryDtoValidation:AbstractValidator<CreateJobCategoryDto>
    {
        public CreateJobCategoryDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name cannot be empty or null")
                .MaximumLength(25).WithMessage("the name must have a maximum of 25 characters")
                .MinimumLength(3).WithMessage("name must have at least 3 characters");
        }
    }
}
