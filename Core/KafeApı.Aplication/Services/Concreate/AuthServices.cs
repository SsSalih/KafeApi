using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.UserDtos;
using KafeApı.Aplication.Helpers;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Concreate
{
    public class AuthServices : IAuthServices
    {
		private readonly TokenHelpers _tokenHelpers;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthServices> _logger; //burasi sonradan eklendi


        public AuthServices(TokenHelpers tokenHelpers, IUserRepository userRepository, ILogger<AuthServices> logger)
        {
            _tokenHelpers = tokenHelpers;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto<object>> GenerateToken(LoginDto dto)
        {
			try
			{
                

                // ✅ Input validation

                if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Email ve şifre gereklidir",
                        ErrorCode = ErrorCodes.Exception
                    };
                }

                // ✅ Kullanıcı kontrolü
                var checkUser = await _userRepository.CheckUser(dto.Email);
                if (string.IsNullOrEmpty(checkUser.Id))
                {
                    _logger.LogWarning("Login attempt for non-existent user: {Email}", dto.Email);
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Kullanıcı bulunamadı", 
                        ErrorCode = ErrorCodes.Exception
                    };
                }

                // ✅ Şifre kontrolü
                var passwordCheck = await _userRepository.CheckUserWithPassword(dto);
                if (!passwordCheck.Succeeded)
                {
                    _logger.LogWarning("Failed login attempt for user: {Email}", dto.Email);
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Email veya şifre hatalı", // ✅ Daha açık mesaj
                        ErrorCode = ErrorCodes.Exception
                    };
                }

                // ✅ Token oluşturma
                var tokenDto = new TokenDto
                {
                    Email = dto.Email,
                    Id = checkUser.Id,
                    Role = checkUser.Roles ?? "User"
                };

                string token = _tokenHelpers.GenereteToken(tokenDto);

                _logger.LogInformation("Successful login for user: {Email}", dto.Email);

                return new ResponseDto<object>
                {
                    Data = new { token = token },
                    Succes = true,
                    Message = "Giriş başarılı"
                };


            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "Error during token generation for email: {Email}", dto?.Email);
                return new ResponseDto<object> { Succes = false, Data = null, Message = "bir hata oluştu", ErrorCode = ErrorCodes.Exception };
			}
        }
    }
}
