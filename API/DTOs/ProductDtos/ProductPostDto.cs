using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.ProductDtos
{
    public class ProductPostDto
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal SoldPrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool DisplayStatus { get; set; }
    }


    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bos bir data daxil etmek olmaz")
                .MaximumLength(20).WithMessage("Zehmet olmasa 20 xarakterden cox data daxil etmeyin");
            RuleFor(x => x.SoldPrice).GreaterThanOrEqualTo(0).WithMessage("Zehmet olmasa 0 ve ya boyuk bir eded daxil edin")
                .LessThanOrEqualTo(9999.99m).WithMessage("Zehmet olmasa 10,000 - den kicik bir eded daxil edin");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.CostPrice > x.SoldPrice)
                {
                    context.AddFailure("CostPrice", "Zehmet olmasa mehsulun maya deyerini satish deyerinden cox yazmayin");
                }
            });
        }
    }
}
