using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface IReviewServices
    {
        Task<ResponseDto<List<ResultReviewDto>>> GetAllReview();
        Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id);
        Task<ResponseDto<object>> AddReview(CreateReviewDto dto);
        Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto);
        Task<ResponseDto<object>> DeleteReview(int id);
    }
}
