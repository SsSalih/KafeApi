using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
        Task <ResponseDto<List<DetailCategoryDto>>> GetByIdCategory(int id);
        Task<ResponseDto<object>> AddCategory(CreatCategoryDto dto);
        Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<object>> DeleteCategory(int id);
        Task<ResponseDto<List<ResultCategoriesWithMenuDto>>> GetCategoriesWithMenuItem();
    }
}
