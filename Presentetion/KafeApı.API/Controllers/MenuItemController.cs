using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
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
            if (!cateogries.Succes)
            {
                if (cateogries.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(cateogries);
                }
                return BadRequest(cateogries);
            }
            return Ok(cateogries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailMenuItem(int id)
        {

            var category = await _menuItemServices.GetDetailMenuItemDto(id);
            if (!category.Succes)
            {
                if (category.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(category);
                }
                return BadRequest(category);
            }
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            

            var result = await _menuItemServices.UpdateMenuItem(dto);
            if (!result.Succes) 
            {
                if(result.ErrorCodes is ErrorCodes.NotFound or ErrorCodes.ValidationError)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var entity = await _menuItemServices.AddMenuItem(dto);
            if (!entity.Succes)
            {
                if (entity.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(entity);
                }
                return BadRequest(entity);
            }
            return Ok(entity);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {

            var result = await _menuItemServices.DelteMenuItem(id);
            if (!result.Succes)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
            return Ok(result);

        }
    }
}
