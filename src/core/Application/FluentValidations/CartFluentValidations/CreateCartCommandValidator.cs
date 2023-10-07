

using Application.Features.Carts.Commands;
using Application.Features.Categories.Commands;
using FluentValidation;

namespace Application.FluentValidations.CartFluentValidations
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktar alanı boş olamaz.");

            

            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id alanı boş olamaz.");

     
        }
    }
}
