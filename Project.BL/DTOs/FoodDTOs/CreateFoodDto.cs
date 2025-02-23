using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Project.BL.Utilites;
using Project.DAL.Models;

namespace Project.BL.DTOs.FoodDTOs
{
    public class CreateFoodDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
    public class CreateFoodDtoValidation : AbstractValidator<CreateFoodDto>
    {
        public CreateFoodDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name cannot be empty or null")
                    .MaximumLength(25).WithMessage("the name must have a maximum of 25 characters")
                    .MinimumLength(3).WithMessage("name must have at least 3 characters");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description cannot be empty or null")
                .MaximumLength(255).WithMessage("the Description must have a maximum of 255 characters")
                .MinimumLength(5).WithMessage("Description must have at least 5 characters");
            RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("Price cannot be empty or null")
               .GreaterThan(0).WithMessage("price starts from 0");
            RuleFor(x => x.ImageUrl)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Image cannot be null!")
            .Must(x => x.Length <= 2 * 1024 * 1024).WithMessage("File size must be less than 2 MB!")
            .Must(x => x.CheckType("image")).WithMessage("File must be image!"); ;

        }
    }
}
