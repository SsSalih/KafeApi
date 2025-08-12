using AutoMapper;
using FluentValidation;
using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Interfaces;
using KafeApı.Aplication.Services.Abstract;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KafeApı.Aplication.Services.Concreate
{
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<OrderItem> _orderItemsRepository;
        private readonly IOrderRepository _orderRepository2;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createValidate;
        private readonly IValidator<UpdateOrderDto> _updateValidate;

        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createValidate, IValidator<UpdateOrderDto> updateValidate, IGenericRepository<OrderItem> orderItemsRepository, IOrderRepository orderRepository2, IGenericRepository<MenuItem> menuItemRepository, IGenericRepository<Table> tableRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _createValidate = createValidate;
            _updateValidate = updateValidate;
            _orderItemsRepository = orderItemsRepository;
            _orderRepository2 = orderRepository2;
            _menuItemRepository = menuItemRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            try
            {
                var validate = await _createValidate.ValidateAsync(dto);

                if (!validate.IsValid) 
                {
                    return new ResponseDto<object>
                    {
                        Succes = false,
                        Message = string.Join(" | ",validate.Errors.Select(x => x.ErrorMessage)),
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                
                var entity =  _mapper.Map<Order>(dto);
                entity.Status = OrderStatus.Hazırlanıyor;
                decimal totalPrice = 0;
                entity.CreatAt=DateTime.Now;
                foreach (var item in entity.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                entity.TotalPrice = totalPrice;
                await _orderRepository.AddAsync(entity);

                var table = await _tableRepository.GetByIdAsync(dto.TableId);
                table.IsActive = false;
                await _tableRepository.UpdateAsync(table);

                return new ResponseDto<object> { Data = null,Succes= true,Message="Ürün eklemiştir" };

            }
            catch (Exception)
            {

                return new ResponseDto<object> 
                {
                    Succes= false,
                    Message="Bir hata Meydana geldi",
                    Data =null,
                    ErrorCode= ErrorCodes.Exception
                };
            }
        }

        //public async Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto)
        //{
        //    try
        //    {
              
        //        var checkOrder = await _orderRepository.GetByIdAsync(dto.OrderId);
        //        var orderItem = await _orderItemsRepository.GetAllAsync();
        //        if (checkOrder == null)
        //        {
        //            return new ResponseDto<object>
        //            {
        //                Data = null,
        //                Succes = false,
        //                Message = "aranan ürün bulunamadı",
        //                ErrorCode = ErrorCodes.NotFound
        //            };
        //        }

        //        var result = _mapper.Map<OrderItem>(dto.OrderItem);
        //        checkOrder.OrderItems.Add(result);
        //        await _orderRepository.UpdateAsync(checkOrder);
                


        //        return new ResponseDto<object>
        //        {
        //            Data = null,
        //            Succes = true,
        //            Message = "ürün başarıyyla güncellendi"
        //        };
        //    }
        //    catch (Exception)
        //    {

        //        return new ResponseDto<object>
        //        {
        //            Data = null,
        //            Succes = false,
        //            Message = "bir hata oluştu",
        //            ErrorCode = ErrorCodes.Exception
        //        };
        //    }
        //}

        public async Task<ResponseDto<object>> DeleteOrder(int id)
        {
            try
            {
                var checkOrder = await _orderRepository.GetByIdAsync(id);
                if (checkOrder == null)
                {
                    return new ResponseDto<object> { Data = null, Succes = false, ErrorCode = ErrorCodes.NotFound, Message = "ürün bulunamadı" };
                }

                await _orderRepository.DeleteAsync(checkOrder);
                checkOrder.Status = OrderStatus.IptalEdildi;
                checkOrder.UpdateAt= DateTime.Now;
                return new ResponseDto<object> { Succes = true, Data = null, Message = "başarılı şekilde silindi" };
            }
            catch (Exception)
            {
                return new ResponseDto<object> { Succes = false, Data = null, Message = "hata oluştu",ErrorCode= ErrorCodes.Exception };

            }

        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var order = await _orderRepository.GetAllAsync();
                var orderItems = await _orderItemsRepository.GetAllAsync();
                if (order == null)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Ürün bulunamadı",
                        Succes = false,
                        Data = null
                    };
                }

                var resultt = _mapper.Map<List<ResultOrderDto>>(order);
                return new ResponseDto<List<ResultOrderDto>> { Data = resultt, Succes = true };

            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultOrderDto>> { Data = null, Succes = false,Message="bir hata oluştu",ErrorCode=ErrorCodes.Exception };

            }

        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrdersWidthDetail()
        {
            try
            {
                var order = await _orderRepository2.GetAllOrderWidthDetailAsync();
                //var orderItems = await _orderItemsRepository.GetAllAsync();
                if (order == null)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Ürün bulunamadı",
                        Succes = false,
                        Data = null
                    };
                }

                var resultt = _mapper.Map<List<ResultOrderDto>>(order);
                return new ResponseDto<List<ResultOrderDto>>{ Data = resultt, Succes = true };

            }
            catch (Exception)
            {

                return new ResponseDto<List<ResultOrderDto>> { Data = null, Succes = false, Message = "bir hata oluştu", ErrorCode = ErrorCodes.Exception };

            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetByIdOrder(int id)
        {
            try
            {
                var order = await _orderRepository2.GetOrderByIdWidthDetailAsync(id);
                if (order == null) 
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Data = null,
                        Succes=false,
                        Message="ürün bulunamadı",
                        ErrorCode=ErrorCodes.NotFound
                    };
                }

                var entityy = _mapper.Map<DetailOrderDto>(order);
                return new ResponseDto<DetailOrderDto> { Data = entityy,Succes= true,Message="ürün getirildi" };
            }
            catch (Exception)
            {

                return new ResponseDto<DetailOrderDto>
                {
                    Data = null,
                    Succes = false,
                    Message = "hata meydana geldi",
                    ErrorCode = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var validate = await _updateValidate.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var checkOrder = await _orderRepository.GetByIdAsync(dto.Id);
                if (checkOrder == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "aranan ürün bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var result = _mapper.Map(dto, checkOrder);

                var orderItemsResult = _mapper.Map(dto.OrderItems, result.OrderItems);

                decimal totalPrice = 0;

                result.UpdateAt = DateTime.Now;

                foreach (var item in result.OrderItems) 
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                result.TotalPrice = totalPrice;
                await _orderRepository.UpdateAsync(result);


                return new ResponseDto<object>
                    {
                    Data = null,
                        Succes = true,
                        Message = "ürün başarıyyla güncellendi"
                    };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
        {
            try
            {
               
                var checkOrder = await _orderRepository.GetByIdAsync(orderId);
                if (checkOrder == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "aranan ürün bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                checkOrder.Status = OrderStatus.Hazir;

                await _orderRepository.UpdateAsync(checkOrder);
              


                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = true,
                    Message = "Hazır olarak güncellendi"
                }
                ;
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId)
        {
            try
            {

                var checkOrder = await _orderRepository.GetByIdAsync(orderId);
                if (checkOrder == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "aranan ürün bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                checkOrder.Status = OrderStatus.IptalEdildi;

                await _orderRepository.UpdateAsync(checkOrder);



                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = true,
                    Message = "İptal Edildi olarak güncellendi"
                }
                ;
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            try
            {

                var checkOrder = await _orderRepository.GetByIdAsync(orderId);
                if (checkOrder == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "aranan ürün bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                checkOrder.Status = OrderStatus.TeslimEdildi;

                await _orderRepository.UpdateAsync(checkOrder);



                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = true,
                    Message = "Ürün teslim edildi olarak güncellendi"
                }
                ;
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusOdendi(int orderId)
        {
            try
            {

                var checkOrder = await _orderRepository.GetByIdAsync(orderId);
                if (checkOrder == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Succes = false,
                        Message = "aranan ürün bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                checkOrder.Status = OrderStatus.Odendi;
                await _orderRepository.UpdateAsync(checkOrder);
                var table = await _tableRepository.GetByIdAsync(checkOrder.TableId);
                table.IsActive = true;
                await _tableRepository.UpdateAsync(table);


                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = true,
                    Message = "Ödendi olarak güncellendi"
                }
                ;
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Succes = false,
                    Message = "bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
