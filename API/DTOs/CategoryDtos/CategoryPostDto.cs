using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
    }


    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bos bir data daxil etmek olmaz")
                .MaximumLength(20).WithMessage("Zehmet olmasa 20 xarakterden cox data daxil etmeyin");
        }
    }
}
