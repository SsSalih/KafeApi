using FluentValidation;
using KafeApı.Aplication.DTOS.TableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Table
{
    public class UpdateTableValidator:AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty()
                .WithMessage("Tablo numarası boş olamaz")
                .GreaterThan(0)
                .WithMessage("Tablo nuumarası 0 dan büyük olmalıdır.");
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Kapasite Boş Olamaz")
                .GreaterThan(0)
                .WithMessage("Kapasie  dan büyük olmalıdır.");
        }
    }
}
