    using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAllCategories();
            if(!categories.Succes)
            {
               if(categories.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(categories);
                }
                return BadRequest(categories);
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var category = await _categoryServices.GetByIdCategory(id);
            if(!category.Succes)
            {
                if (category.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(category);
                }
                return BadRequest(category);
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreatCategoryDto dto) 
        {
            var entity = await _categoryServices.AddCategory(dto);
            if (!entity.Succes)
            {
                if(entity.ErrorCodes == ErrorCodes.ValidationError) 
                {
                    return Ok(entity);
                }

                return BadRequest(entity);
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            

            var result =await _categoryServices.UpdateCategory(dto);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var result =await _categoryServices.DeleteCategory(id);
            if(!result.Succes)
            {
                if(result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return NotFound(result);
                }

                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
