using FluentValidation;
using KafeApı.Aplication.DTOS.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("kategori Id boş olamaz.")
                .GreaterThan(0).WithMessage("Kategori Id 0'dan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .Length(2, 50).WithMessage("Kategori adı 2 ile 50 karakter arasında olmalıdır.");

        }
    }
}
