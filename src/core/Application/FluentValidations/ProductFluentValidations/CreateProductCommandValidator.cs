

using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.FluentValidations.ProductFluentValidations
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması boş olamaz.");
            RuleFor(x => x.Stock).GreaterThan(0).WithMessage("Stok miktarı 0'dan büyük olmalıdır.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Geçerli bir kategori seçmelisiniz.");
           
        }
    }
}
