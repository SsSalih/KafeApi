using KafeApı.Aplication.DTOS.MenuItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.CategoryDtos
{
    public class DetailCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<ResultMenuItemDto> MenuItems { get; set; }
    }
}
