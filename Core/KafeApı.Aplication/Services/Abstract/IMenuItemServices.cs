using KafeApı.Aplication.DTOS.CategoryDtos;
using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.MenuItemsDtos;
using KafeApı.Aplication.DTOS.ResponseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
       
        Task <ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto);
        Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto);
        Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
        Task<ResponseDto<object>> DelteMenuItem(int id);
    }
}
