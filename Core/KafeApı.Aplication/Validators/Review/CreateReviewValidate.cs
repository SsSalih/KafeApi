using FluentValidation;
using KafeApı.Aplication.DTOS.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Validators.Review
{
    public class CreateReviewValidate : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidate()
        {
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Yorum bos olamaz").Length(5,500).WithMessage("yorum en az 5 en fazla 500 karakter iceraebilir");

            RuleFor(x => x.Rating)
             .NotEmpty().WithMessage("Puanlama boş bırakılamaz")
             .Must(rating => new[] { "1", "2", "3", "4", "5" }.Contains(rating))
             .WithMessage("Rating 1 ile 5 arasında olmalıdır");
        }
    }
}
