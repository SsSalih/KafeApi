using KafeApı.Aplication.DTOS.AuthDtos;
using KafeApı.Aplication.DTOS.OrderDtos;
using KafeApı.Aplication.Helpers;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace KafeApı.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderServices;
        

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
            
        }

        [Authorize]
        [HttpGet("debug")]
        public IActionResult Debug()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var isAuthenticated = User.Identity.IsAuthenticated;
            var isInAdminRole = User.IsInRole("admin");
            var isInEmployeRole = User.IsInRole("employe");

            return Ok(new
            {
                IsAuthenticated = isAuthenticated,
                Claims = claims,
                IsInAdminRole = isInAdminRole,
                IsInEmployeRole = isInEmployeRole
            });
        }


        [Authorize(Roles = "admin,employe")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            // ✅ Token bilgilerini bu şekilde alın
            var userEmail = User.FindFirst("_e")?.Value;
            var userId = User.FindFirst("_u")?.Value;
            var userRole = User.FindFirst("role")?.Value;

            Console.WriteLine($"User: {userEmail}, Role: {userRole}");


            var order = await _orderServices.GetAllOrder();
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto)
        {
            var order = await _orderServices.AddOrder(dto);
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id) 
        {
            var order = await _orderServices.DeleteOrder(id);
            return CreateResponse(order);
        }


        [Authorize(Roles = "admin,employe")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id) 
        {
            var order =await _orderServices.GetByIdOrder(id);
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var order = await _orderServices.UpdateOrder(dto);
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet("withdetails")]
        public async Task<IActionResult> GetAllOrdersWidthDetail() 
        {
            var order =await _orderServices.GetAllOrdersWidthDetail();
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/hazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId) 
        {
            var order = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/iptaledilidi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int orderId)
        {
            var order = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(order);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/tesilimedildi")]
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

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/odendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId) 
        {
            var order = await _orderServices.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(order);
        }
    }
}
