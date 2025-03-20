using EntityLayer.Entities;
using FluentValidation;

namespace ServiceLayer.FluentValidations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.NameSurname)
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(100)
              .WithName("Ad Soyad");
            RuleFor(x => x.PhoneNumber)
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(11)
              .WithName("Telefon Numarası");
            RuleFor(x => x.Email)
              .NotEmpty()
              .MinimumLength(3)
              .MaximumLength(100)
              .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
              .WithName("Email");
            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(100)
               .WithName("Şifre");
        }
    }
}
