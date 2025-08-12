using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Helpers;
using KafeApı.Aplication.Services.Abstract;
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

        public AuthServices(TokenHelpers tokenHelpers)
        {
            _tokenHelpers = tokenHelpers;
        }

        public async Task<ResponseDto<object>> GenerateToken(TokenDto dto)
        {
			try
			{
                string token = _tokenHelpers.GenereteToken(dto);
                return new ResponseDto<object> { Data =token, Succes = true };
			}
			catch (Exception ex)
			{

				return new ResponseDto<object> { Succes = false, Data = null, Message = "bir hata oluştu", ErrorCode = ErrorCodes.Exception };
			}
        }
    }
}
