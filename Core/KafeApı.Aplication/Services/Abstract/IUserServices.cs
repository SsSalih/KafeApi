using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface IUserServices
    {
        Task<ResponseDto<object>> Register(RegisterDto dto);
        Task<ResponseDto<object>> RegisterDefault(RegisterDto dto);
        Task<ResponseDto<object>> CreateRole(string roleName);
        Task<ResponseDto<object>> AddToRole(string email, string roleName);
    }
}
