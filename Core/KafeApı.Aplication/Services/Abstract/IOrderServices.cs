using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface IOrderServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder();
        Task<ResponseDto<DetailOrderDto>> GetByIdOrder(int id);
        Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<object>> DeleteOrder(int id);
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrdersWidthDetail();
        Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusOdendi(int orderId);

        //Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto);
    }
}
