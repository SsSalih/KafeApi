using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : BaseController
    {
        private readonly IOrderItemServices _orderItemServices;

        public OrderItemController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems() 
        {
            var orderItems = await _orderItemServices.GetAllOrderItems();
            return CreateResponse(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id) 
        {
            var orderItems = await _orderItemServices.GetOrderItemById(id);
            return CreateResponse(orderItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem(CreateOrderItemDto dto) 
        {
            var orderItem = await _orderItemServices.AddOrderItem(dto);
            return CreateResponse(orderItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(int id) 
        {
            var orderItem = await _orderItemServices.DeleteOrderItem(id);
            return CreateResponse(orderItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto) 
        {
            var orderItem = await _orderItemServices.UpdateOrderItem(dto);
            return CreateResponse(orderItem);
        }
    }
}
