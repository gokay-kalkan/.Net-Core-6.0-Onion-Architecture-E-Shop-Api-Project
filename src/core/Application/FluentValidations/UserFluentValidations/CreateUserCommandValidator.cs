

using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.FluentValidations.UserFluentValidations
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
                RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
                RuleFor(x=>x.City).NotEmpty().WithMessage("Şehir alanı boş geçilemez");
                RuleFor(x=>x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez");
                RuleFor(x=>x.UserName).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez");
                RuleFor(x=>x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
        }
    }
}
