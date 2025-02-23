using FluentValidation;

namespace Project.BL.DTOs.JobCategoryDTOs
{
    public class UpdateJobCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateeJobCategoryDtoValidation : AbstractValidator<UpdateJobCategoryDto>
    {
        public UpdateeJobCategoryDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name cannot be empty or null")
                .MaximumLength(25).WithMessage("the name must have a maximum of 25 characters")
                .MinimumLength(3).WithMessage("name must have at least 3 characters");
        }
    }
}
