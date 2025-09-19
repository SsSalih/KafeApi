using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.UserDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Aplication.Validators.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KafeApı.Aplication.Services.Concreate
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterValidator _registerValidate;

        public UserServices(IUserRepository userRepository, RegisterValidator registerValidate)
        {
            _userRepository = userRepository;
            _registerValidate = registerValidate;
        }

        public async Task<ResponseDto<object>> AddToRole(string email, string roleName)
        {
            try
            {
                var result = await _userRepository.AddRoleToUserAsync(email, roleName);
                if (result)
                    return new ResponseDto<object> { Data = null, Succes = true, Message = "role ataması yapıldı" };
                return new ResponseDto<object> { Data = null, Succes = false, Message = "hata oluştu", ErrorCode = ErrorCodes.Exception };
            }
            catch (Exception)
            {


                return new ResponseDto<object> { Data = null, Succes = false, Message = "bir hata oluştu", ErrorCode = ErrorCodes.BadRequest };
            }
        }

        public async Task<ResponseDto<object>> CreateRole(string roleName)
        {
            try
            {
                var result = await _userRepository.CreateRoleAsync(roleName);
                if (result)
                    return new ResponseDto<object> { Data = null, Succes = true, Message = "role oluşturuldu" };
                return new ResponseDto<object> { Data = null, Succes = false, Message = "hata oluştu", ErrorCode = ErrorCodes.Exception };
            }
            catch (Exception)
            {

                return new ResponseDto<object> { Data = null, Succes = false, Message = "bir hata oluştu", ErrorCode = ErrorCodes.BadRequest };
            }
        }

        public async Task<ResponseDto<object>> Register(RegisterDto dto)
        {
            try
            {
                var validate =_registerValidate.Validate(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Data = null, Succes = false, Message = "validasyon hatası", ErrorCode = ErrorCodes.ValidationError };
                }
                var result = await _userRepository.RegisterAsync(dto);
                if (result.Succeeded)
                {
                    return new ResponseDto<object> { Data = null, Succes = true, Message = "Kayıt başarılı" };
                }

                else 
                {
                    return new ResponseDto<object> { Data = null, Succes = false, Message = result.Errors.FirstOrDefault().Description};
                }
            }
            catch (Exception)
            {

                return new ResponseDto<object> { Data = null, Succes = false, Message = "bir hata oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> RegisterDefault(RegisterDto dto)
        {
            try
            {
            var validate = _registerValidate.Validate(dto);
            if (!validate.IsValid)
            {
                return new ResponseDto<object> { Data = null, Succes = false, Message = "validasyon hatası", ErrorCode = ErrorCodes.ValidationError };
            }

               var register = await _userRepository.RegisterAsync(dto);
                if (register.Succeeded)
                {
                var addRole = await _userRepository.AddRoleToUserAsync(dto.Email, "User");
                    if (!addRole)
                    {
                        return new ResponseDto<object> { Data = null, Succes = false, Message = "role atama hatası", ErrorCode = ErrorCodes.Exception };
                    }

                    return new ResponseDto<object> { Data = null, Succes = true, Message = "Kayıt başarılı" };

                }
                else
                {
                    return new ResponseDto<object> { Data = null, Succes = false, Message = register.Errors.FirstOrDefault().Description };
                }

            }
            catch (Exception)
            {

                return new ResponseDto<object> { Data = null, Succes = false, Message = "bir hata oluştu", ErrorCode = ErrorCodes.Exception };

            }
        }
    }
}
