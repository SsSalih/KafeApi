using KafeApı.Aplication.DTOS.UserDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto) 
        {
            var result = await _userServices.Register(dto); 
            return CreateResponse(result);  
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(string roleName) 
        {
            var result = await _userServices.CreateRole(roleName);
            return CreateResponse(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("addToRole")]
        public async Task<IActionResult> AddToRole(string email, string roleName)
        {
            var result = await _userServices.AddToRole(email,roleName);
            return CreateResponse(result);
        }

        [HttpPost("registerDefault")]
        public async Task<IActionResult> RegisterDefault(RegisterDto dto)
        {
            var result = await _userServices.RegisterDefault(dto);
            return CreateResponse(result);
        }

    }
}
