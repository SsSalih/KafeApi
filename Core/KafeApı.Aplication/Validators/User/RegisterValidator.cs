using FluentValidation;
using KafeApı.Aplication.DTOS.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.User
{
    public class RegisterValidator :AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email alanı boş olamaz")
                .EmailAddress()
                .WithMessage("Geçersiz email adresi");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ad alanı boş olamaz")
                .MinimumLength(2)
                .WithMessage("En az 2 karakter olmalıdır");
            RuleFor(x => x.Surname)
             .NotEmpty()
             .WithMessage("Soyad alanı boş olamaz")
             .MinimumLength(2)
             .WithMessage("En az 2 karakter olmalıdır");

            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage("Ad alanı boş olamaz")
            //    .MinimumLength(6)
            //    .WithMessage("şifre alanı en az 6 karakter olmalıdır")
            //    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            //    .WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir");



        }
    }
}
