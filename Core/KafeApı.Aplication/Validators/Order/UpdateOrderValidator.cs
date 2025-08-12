using FluentValidation;
using KafeApı.Aplication.DTOS.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            //RuleFor(x => x.TotalPrice)
            //    .NotEmpty()
            //    .WithMessage("boş olamaz")
            //    .GreaterThan(0)
            //    .WithMessage("sipariş ücreti 0 dan büyük olmak zorunda");

        }
    }
}
