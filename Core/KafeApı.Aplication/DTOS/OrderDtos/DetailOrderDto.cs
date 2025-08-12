using KafeApı.Aplication.DTOS.OrderItemDtos;
using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.DTOS.OrderDtos
{
    public class DetailOrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Status { get; set; }//enum değer yazıcaz
        public List<ResultOrderItemDto> OrderItems { get; set; }
    }
}
