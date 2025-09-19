using FluentValidation;
using KafeApı.Aplication.DTOS.CafeInfoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.CafeInfo
{
    public class AddCafeInfoValidate : AbstractValidator<CreateCafeInfoDto>
    {
        public AddCafeInfoValidate() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Kafe adı boş geçilemez.")
                .MaximumLength(100)
                .WithMessage("Kafe adı en fazla 100 karakter olabilir.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Açıklama boş geçilemez.")
                .MaximumLength(500)
                .WithMessage("Açıklama en fazla 500 karakter olabilir.");
            RuleFor(x => x.Adress)
                .NotEmpty()
                .WithMessage("Adres boş geçilemez.")
                .MaximumLength(200)
                .WithMessage("Adres en fazla 200 karakter olabilir.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Telefon numarası boş geçilemez.")
                .MaximumLength(15)
                .WithMessage("Telefon numarası en fazla 15 karakter olabilir.")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Geçersiz telefon numarası formatı.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email boş geçilemez.")
                .MaximumLength(100)
                .WithMessage("Email en fazla 100 karakter olabilir.")
                .EmailAddress()
                .WithMessage("Geçersiz email formatı.");


        }
    }
}
