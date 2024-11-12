using FluentValidation;

namespace ERPServer.Application.Features.Auth.Register
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.UserName)
                .MinimumLength(3)
                .WithMessage("Kullanıcı adı en az 3 karakter olmalıdır");

            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi girin");

            RuleFor(p => p.Password)
                .MinimumLength(6)
                .WithMessage("Şifre en az 6 karakter olmalıdır");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("Ad zorunludur");

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage("Soyad zorunludur");
        }
    }
}
