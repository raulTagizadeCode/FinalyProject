using FluentValidation;
using Project.BL.DTOs.FoodDTOs;

namespace Project.BL.DTOs.CategoryDTOs
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
    }
    public class CreateCategoryDtoValidation : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name cannot be empty or null")
                .MaximumLength(25).WithMessage("the name must have a maximum of 25 characters")
                .MinimumLength(3).WithMessage("name must have at least 3 characters");
          

        }
    }
    public class CreateOrderCategoryDto
    {
        public string Name { get; set; }
    }
}
