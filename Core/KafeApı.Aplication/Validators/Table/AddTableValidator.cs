using FluentValidation;
using FluentValidation.Validators;
using KafeApı.Aplication.DTOS.TableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Table
{
    public class AddTableValidator : AbstractValidator<CreatTableDto>
    {
        public AddTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty()
                .WithMessage("Table number cannot be empty.")

                .GreaterThan(0)
                .WithMessage("Table number must be greater than 0.");

            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Capacity cannot be empty.")

                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive status must be specified.");
        }
    }
}
