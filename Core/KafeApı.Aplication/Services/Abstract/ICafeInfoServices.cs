using KafeApı.Aplication.DTOS.CafeInfoDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface ICafeInfoServices
    {
        Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfo();
        Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id);                           
        Task<ResponseDto<object>> AddCafeInfoDto(CreateCafeInfoDto dto);
        Task<ResponseDto<object>> UpdateCafeInfoDto(UpdateCafeInfoDto dto);
        Task<ResponseDto<object>> DeleteCafeInfo(int id);   
    }
}
