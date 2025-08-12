    using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
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
           return CreateResponse(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var category = await _categoryServices.GetByIdCategory(id);
            return CreateResponse(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreatCategoryDto dto) 
        {
            var entity = await _categoryServices.AddCategory(dto);
            return CreateResponse(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
        {
            

            var result =await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var result =await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }

        [HttpGet("getCategoriesWithMenuItem")]
        public async Task<IActionResult> GetCategoriesWithMenuItem()
        {
            var result = await _categoryServices.GetCategoriesWithMenuItem();
            return CreateResponse(result);
        }
        
    }
}
