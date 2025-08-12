using FluentValidation;
using FluentValidation.Validators;
using KafeApı.Aplication.DTOS.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.OrderItem
{
    public class AddOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public AddOrderItemValidator()
        {
            
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Sipariş adedi boş olamaz")
                .GreaterThan(0)
                .WithMessage("sipariş adedi 0dan büyük olamlı .");
        }
    }
}
