using FluentValidation;
using KafeApı.Aplication.DTOS.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Category
{
    public class AddCategoryValidator: AbstractValidator<CreatCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(2, 50).WithMessage("Kategori adı 2 ile 50 karakter arasında olmalıdır.");
        }
    }
}
