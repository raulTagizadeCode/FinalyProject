using FluentValidation;

namespace Project.BL.DTOs.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCategoryDtoValidation : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name cannot be empty or null")
                .MaximumLength(25).WithMessage("the name must have a maximum of 25 characters")
                .MinimumLength(3).WithMessage("name must have at least 3 characters");


        }
    }
    public class UpdateOrderCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
