using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("generateToken")]
        public async Task<IActionResult> GenarateToken(TokenDto dto) 
        {
            var result = await _authServices.GenerateToken(dto);
            return CreateResponse(result);
        }
    }
}
