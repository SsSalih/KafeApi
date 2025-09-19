using AutoMapper;
using KafeApı.Aplication.DTOS.CafeInfoDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Concreate
{
    public class CafeInfoServices : ICafeInfoServices
    {
        private readonly IGenericRepository<CafeInfo> _cafeInfo;
        private readonly IMapper _mapper;
        private readonly Validators.CafeInfo.AddCafeInfoValidate _addCafeInfoValidate;
        private readonly Validators.CafeInfo.UpdateCafeInfoValidate _updateCafeInfoValidate;

        public CafeInfoServices(IGenericRepository<CafeInfo> cafeInfo, IMapper mapper, Validators.CafeInfo.AddCafeInfoValidate addCafeInfoValidate, Validators.CafeInfo.UpdateCafeInfoValidate updateCafeInfoValidate)
        {
            _cafeInfo = cafeInfo;
            _mapper = mapper;
            _addCafeInfoValidate = addCafeInfoValidate;
            _updateCafeInfoValidate = updateCafeInfoValidate;
        }
        public async Task<ResponseDto<object>> AddCafeInfoDto(CreateCafeInfoDto dto)
        {
            try
            {
                var validationResult = await _addCafeInfoValidate.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object> { Succes = false, Data = dto, Message = "Ekleme işlemi başarısız validasyon hatasi", ErrorCode = ErrorCodes.ValidationError };
                }
                var entities =  _mapper.Map<CafeInfo>(dto);

                if (entities == null) 
                {
                    return new ResponseDto<object> { Succes = false, Data = dto, Message = "Ekleme işlemi başarısız", ErrorCode = ErrorCodes.Exception };
                }

                await _cafeInfo.AddAsync(entities);
                return new ResponseDto<object> { Succes = true, Data = dto, Message = "Ekleme işlemi başarılı", ErrorCode = null };
                }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseDto<object>> DeleteCafeInfo(int id)
        {
            try
            {
                var result = await _cafeInfo.GetByIdAsync(id);
                if (result == null)
                {
                    return new ResponseDto<object> { Succes = false, Data = null, Message = "Silme işlemi başarısız", ErrorCode = ErrorCodes.NotFound };
                }

                await _cafeInfo.DeleteAsync(result);
                return new ResponseDto<object> { Succes = true, Data = null, Message = "Silme işlemi başarılı", ErrorCode = null };
            }
            catch (Exception)
            {

                return new ResponseDto<object> { Succes = false, Data = null, Message = "Silme işlemi sırasında bir hata oluştu", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfo()
        {
            try
            {
                var cafeInfos = await _cafeInfo.GetAllAsync();
                if (cafeInfos == null)
                {
                    return new ResponseDto<List<ResultCafeInfoDto>>
                    {
                        Succes = false,
                        Data = null,
                        Message = "Kafe bilgisi bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var dto = _mapper.Map<List<ResultCafeInfoDto>>(cafeInfos);
                return new ResponseDto<List<ResultCafeInfoDto>>
                {
                    Succes = true,
                    Data = dto,
                    Message = "Kafe bilgileri başarıyla getirildi",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultCafeInfoDto>>
                {
                    Succes = false,
                    Data = null,
                    Message = "Kafe bilgileri getirilirken bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id)
        {
            try
            {
                var cafeInfo =await _cafeInfo.GetByIdAsync(id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<DetailCafeInfoDto>
                    {
                        Succes = false,
                        Data = null,
                        Message = "Kafe bilgisi bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var dto = _mapper.Map<DetailCafeInfoDto>(cafeInfo);
                return new ResponseDto<DetailCafeInfoDto>
                {
                    Succes = true,
                    Data = dto,
                    Message = "Kafe bilgisi başarıyla getirildi",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailCafeInfoDto>
                {
                    Succes = false,
                    Data = null,
                    Message = "Kafe bilgisi getirilirken bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
          

        }

        public async Task<ResponseDto<object>> UpdateCafeInfoDto(UpdateCafeInfoDto dto)
        {
            try
            {
                var validate = await _updateCafeInfoValidate.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Succes = false, Data = dto, Message = "Güncelleme işlemi başarısız validasyon hatasi", ErrorCode = ErrorCodes.ValidationError };
                }
                var cafeInfo = await _cafeInfo.GetByIdAsync(dto.Id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<object> { Succes = false, Data = dto, Message = "Güncelleme işlemi başarısız", ErrorCode = ErrorCodes.NotFound };
                }

                var entities = _mapper.Map(dto, cafeInfo);
                await _cafeInfo.UpdateAsync(entities);
                return new ResponseDto<object> { Succes = true, Data = dto, Message = "Güncelleme işlemi başarılı", ErrorCode = null };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Succes = false,
                    Data = dto,
                    Message = "Güncelleme işlemi sırasında bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
