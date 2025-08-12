using KafeApı.Aplication.DTOS.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.OrderDtos
{
    public class CreateOrderDto
    {
        
        public int TableId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime CreatAt { get; set; }= DateTime.Now;
       // public DateTime? UpdateAt { get; set; }
       // public string Status { get; set; }//enum servisten göndericez
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
