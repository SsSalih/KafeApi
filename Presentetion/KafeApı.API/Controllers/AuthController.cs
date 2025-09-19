using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.DTOS.UserDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost]
        public async Task<IActionResult> GenarateToken(LoginDto dto) 
        {
            var result = await _authServices.GenerateToken(dto);
            return CreateResponse(result);
        }

        
    }
}
