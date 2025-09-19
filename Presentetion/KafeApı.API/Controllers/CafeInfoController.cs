using KafeApı.Aplication.DTOS.CafeInfoDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/cafeinfos")]
    [ApiController]
    public class CafeInfoController : BaseController
    {
        private readonly ICafeInfoServices _cafeInfoServices;

        public CafeInfoController(ICafeInfoServices cafeInfoServices)
        {
            _cafeInfoServices = cafeInfoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCafeInfo()
        {
            var result = await _cafeInfoServices.GetAllCafeInfo();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCafeInfo(int id) 
        {
            var result = await _cafeInfoServices.GetByIdCafeInfo(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCafeInfoDto([FromBody] CreateCafeInfoDto dto) 
        {
            var result = await _cafeInfoServices.AddCafeInfoDto(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCafeInfoDto([FromBody] UpdateCafeInfoDto dto) 
        {
            var result = await _cafeInfoServices.UpdateCafeInfoDto(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCafeInfo([FromQuery] int id)// ?id=1 bu sekilde deger atmak icin gerekli
        {
            var result = await _cafeInfoServices.DeleteCafeInfo(id);
            return CreateResponse(result);
        }
    }
}
