using KafeApı.Aplication.DTOS.MenuItemDtos;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.OrderItemDtos
{
    public class ResultOrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public DetailMenuItemDto MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
