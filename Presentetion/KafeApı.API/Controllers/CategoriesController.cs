    using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using KafeApı.Aplication.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeApı.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ILogger<CategoriesController> _log;

        public CategoriesController(ICategoryServices categoryServices, ILogger<CategoriesController> log)
        {
            _categoryServices = categoryServices;
            _log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _log.LogInformation("get-categories");
            var categories = await _categoryServices.GetAllCategories();
            _log.LogInformation("iget-categories: " + categories.Succes);
            _log.LogWarning("wget-categories: " + categories.Succes);
            _log.LogError("eget-categories: " + categories.Succes);
            _log.LogDebug("dget-categories: " + categories.Succes);
            return CreateResponse(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory([FromRoute]int id)// query de ?id=1 routde /1
        {
            var category = await _categoryServices.GetByIdCategory(id); 
            return CreateResponse(category);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreatCategoryDto dto) 
        {
            Console.WriteLine();
            var entity = await _categoryServices.AddCategory(dto);
            return CreateResponse(entity);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            

            var result =await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var result =await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }

        [HttpGet("withmenuitems")]
        public async Task<IActionResult> GetCategoriesWithMenuItem()
        {
            var result = await _categoryServices.GetCategoriesWithMenuItem();
            return CreateResponse(result);
        }
        
    }
}
