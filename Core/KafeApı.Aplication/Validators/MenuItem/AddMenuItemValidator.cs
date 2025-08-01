using FluentValidation;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.MenuItem
{
    public class AddMenuItemValidator :AbstractValidator<CreateMenuItemDto>
    {
        public AddMenuItemValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(2, 50).WithMessage("Kategori adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
            
                .NotEmpty().WithMessage("Kategori açıklaması boş olamaz.")
                .Length(5, 200).WithMessage("Kategori açıklaması 5 ile 200 karakter arasında olmalıdır.");
            
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Kategori fiyatı boş olamaz.")
                .GreaterThan(0).WithMessage("Kategori fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori ID boş olamaz.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Kategori resmi boş olamaz.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Kategori resmi geçerli bir URL olmalıdır.");
        }

    }
}
