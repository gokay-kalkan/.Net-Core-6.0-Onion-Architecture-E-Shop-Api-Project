

using Application.Features.Categories.Commands;
using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.FluentValidations.CategoryFluentValidations
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş olamaz.");
        }
    }
}
