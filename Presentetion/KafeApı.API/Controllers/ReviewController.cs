using KafeApı.Aplication.DTOS.ReviewDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReview()
        {
            var reviews = await _reviewServices.GetAllReview();
            return CreateResponse(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdReview(int id)
        {
            var review = await _reviewServices.GetByIdReview(id);
            return CreateResponse(review);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            var review = await _reviewServices.AddReview(dto);
            return CreateResponse(review);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto dto)
        {
            var review = await _reviewServices.UpdateReview(dto);
            return CreateResponse(review);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewServices.DeleteReview(id);
            return CreateResponse(review);
        }
    }
}
