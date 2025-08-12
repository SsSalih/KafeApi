using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var order = await _orderServices.GetAllOrder();
            return CreateResponse(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var order = await _orderServices.AddOrder(dto);
            return CreateResponse(order);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id) 
        {
            var order = await _orderServices.DeleteOrder(id);
            return CreateResponse(order);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id) 
        {
            var order =await _orderServices.GetByIdOrder(id);
            return CreateResponse(order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var order = await _orderServices.UpdateOrder(dto);
            return CreateResponse(order);
        }

        [HttpGet("getAllOrdersWithDetail")]
        public async Task<IActionResult> GetAllOrdersWidthDetail() 
        {
            var order =await _orderServices.GetAllOrdersWidthDetail();
            return CreateResponse(order);
        }

        [HttpPut("updateOrderStatusHazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId) 
        {
            var order = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(order);
        }

        [HttpPut("updateOrderStatusIptalEdilidi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int orderId)
        {
            var order = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(order);
        }

        [HttpPut("updateOrderStatusTesilimEdildi")]
        public async Task<IActionResult> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            var order = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(order);
        }

        //[HttpPut("addOrderItemByOrder")]
        //public async Task<IActionResult> AddOrderItemByOrderId(AddOrderItemByOrderDto dto) 
        //{
        //    var order = await _orderServices.AddOrderItemByOrderId(dto);
        //    return CreateResponse(order);

        //}

        [HttpPut("updateOrderStatusOdendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId) 
        {
            var order = await _orderServices.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(order);
        }
    }
}
