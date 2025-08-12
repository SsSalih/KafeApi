using AutoMapper;
using FluentValidation;
using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Concreate
{
    
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;

        public OrderItemServices(IGenericRepository<OrderItem> orderItemRepository, IValidator<CreateOrderItemDto> createOrderItemValidator, IMapper mapper, IValidator<UpdateOrderItemDto> updateOrderItemValidator)
        {
            _orderItemRepository = orderItemRepository;
            _createOrderItemValidator = createOrderItemValidator;
            _mapper = mapper;
            _updateOrderItemValidator = updateOrderItemValidator;
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validator = await _createOrderItemValidator.ValidateAsync(dto);
                if (!validator.IsValid) 
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Data = null,
                        Message = string.Join(" | ", validator.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var entity = _mapper.Map<OrderItem>(dto);
                await _orderItemRepository.AddAsync(entity);

                return new ResponseDto<object>
                {
                    Succes = true,
                    Data = null,
                    Message = "Order item added successfully",
                    
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Succes = false,
                    Data = null,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var order = await _orderItemRepository.GetByIdAsync(id);
                if(order == null) 
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Data = null,
                        Message = "Order item bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };

                }
                await _orderItemRepository.DeleteAsync(order);
                return new ResponseDto<object>
                {
                    Succes = true,
                    Data = null,
                    Message = "Order item başarıyla silindi"
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Succes = false,
                    Data = null,
                    Message = "Order item silinme esnasında bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var order = await _orderItemRepository.GetAllAsync();
                if(order.Count == 0) 
                {
                    return new ResponseDto<List<ResultOrderItemDto>>
                    {
                        Data =null,
                        Succes= false,
                        Message="Listenilecek öğe bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var result = _mapper.Map<List<ResultOrderItemDto>>(order);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Data=result,
                    Succes=true,
                };
            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Data = null,
                    Succes = false,
                    Message = "Bir hata meydana geldi",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if(orderItem == null) 
                {
                    return new ResponseDto<DetailOrderItemDto>
                    {
                        Data = null,
                        Succes = false,
                        Message = "Araanan Öğe Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var response = _mapper.Map<DetailOrderItemDto>(orderItem);
                return new ResponseDto<DetailOrderItemDto>
                {
                    Data = response,
                    Succes = true,
                    Message = "başarılı şekilde Getrilmiştir",

                };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailOrderItemDto>
                {
                    Data = null,
                    Succes = false,
                    Message = "Bir Hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = dto,
                        Succes = false
                    };
                }

                var checkOrderItem = await _orderItemRepository.GetByIdAsync(dto.Id);
                if(checkOrderItem == null) 
                {

                    return new ResponseDto<object>
                    {
                        Message = "sipariş itemi bulunamdı",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = dto,
                        Succes = false
                    };
                }
                var result = _mapper.Map(dto, checkOrderItem);//<> bunu arasında yaydıklarımızda yeni nesne üretiyor bu şekilde chech edilen veriyi dtoya gönderirir

                await _orderItemRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Data = result,
                    Succes = true,
                    Message="başarılı bir şekilde getirldi"
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Message = "hata oluştu",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null,
                    Succes = false
                };
            }
           
        }
    }
}
