using AutoMapper;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.DTOS.ReviewDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Aplication.Validators.Review;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Concreate
{
    public class ReviewServices : IReviewServices
    {
        private readonly IGenericRepository<Review> _genericRepository;
        private readonly IMapper _mapper;
        private readonly CreateReviewValidate _addReviewValidate;
        private readonly UpdateReviewValidate _updateReviewValidate;

        public ReviewServices(IGenericRepository<Review> genericRepository, IMapper mapper, CreateReviewValidate addReviewValidate, UpdateReviewValidate updateReviewValidate)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _addReviewValidate = addReviewValidate;
            _updateReviewValidate = updateReviewValidate;
        }

        public async Task<ResponseDto<object>> AddReview(CreateReviewDto dto)
        {
            try
            {
                var validate = await _addReviewValidate.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> 
                    {
                        Data = null,
                        Succes = false,
                        Message = validate.Errors.First().ErrorMessage,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var result = _mapper.Map<Review>(dto);
                if (result == null) 
                {
                    return new ResponseDto<object> 
                    {
                        Data = null,
                        Succes = false,
                        Message = "Yorum ekleme işlemi başarısız",
                        ErrorCode = ErrorCodes.Exception
                    };
                }
                await _genericRepository.AddAsync(result);
                return new ResponseDto<object> 
                {
                    Data = dto,
                    Succes = true,
                    Message = "Yorum ekleme işlemi başarılı",
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object> 
                {
                    Data = null,
                    Succes = false,
                    Message = "Beklenmedik bir hata olusutu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteReview(int id)
        {
            try
            {
                var result = await _genericRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Aranilan oge bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericRepository.DeleteAsync(result);
                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = true,
                    Message = "Silme işlemi başarılı",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "Beklenmedik bir hata olusutu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
           
        }

        public async Task<ResponseDto<List<ResultReviewDto>>> GetAllReview()
        {
            try
            {
                var entities = await _genericRepository.GetAllAsync();
                if (entities == null || entities.Count == 0)
                {
                    return new ResponseDto<List<ResultReviewDto>>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Herhangi bir yorum bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var dtos = _mapper.Map<List<ResultReviewDto>>(entities);
                return new ResponseDto<List<ResultReviewDto>>
                {
                    Data = dtos,
                    Succes = true,
                    Message = "Yorumlar başarıyla getirildi",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultReviewDto>>
                {
                    Data = null,
                    Succes = false,
                    Message = "Beklenmedik bir hata olusutu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
           
        }

        public async Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id)
        {
            try
            {
                var entity = await _genericRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new ResponseDto<DetailReviewDto>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Aranilan oge bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var dto = _mapper.Map<DetailReviewDto>(entity);
                return new ResponseDto<DetailReviewDto>
                {
                    Data = dto,
                    Succes = true,
                    Message = "Oge başarıyla getirildi",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailReviewDto>
                {
                    Data = null,
                    Succes = false,
                    Message = "Beklenmedik bir hata olusutu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto)
        {
            try
            {
                var validate =await _updateReviewValidate.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = validate.Errors.First().ErrorMessage,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var result = await _genericRepository.GetByIdAsync(dto.Id);
                if (result == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Güncellenecek oge bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var entity = _mapper.Map(dto, result);
                await _genericRepository.UpdateAsync(entity);
                return new ResponseDto<object>
                {
                    Data = dto,
                    Succes = true,
                    Message = "Oge başarıyla güncellendi",
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "Beklenmedik bir hata olusutu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }
    }
}
