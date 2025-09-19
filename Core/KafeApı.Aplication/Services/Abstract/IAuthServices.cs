using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface IAuthServices
    {
        Task<ResponseDto<object>> GenerateToken(LoginDto dto);
    }
}
