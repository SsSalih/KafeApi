using KafeApı.Aplication.DTOS.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.OrderDtos
{
    public class AddOrderItemByOrderDto
    {
        public int OrderId { get; set; }
        public CreateOrderItemDto OrderItem{ get; set; }
    }
}
