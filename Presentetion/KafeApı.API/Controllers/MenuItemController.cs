using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/menuitems")]
    [ApiController]
    public class MenuItemController : BaseController
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var cateogries = await _menuItemServices.GetAllMenuItems();
            return CreateResponse(cateogries);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var result = await _menuItemServices.UpdateMenuItem(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var entity = await _menuItemServices.AddMenuItem(dto);
            return CreateResponse(entity);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {

            var result = await _menuItemServices.DelteMenuItem(id);
            return CreateResponse(result);

        }

    }
}
