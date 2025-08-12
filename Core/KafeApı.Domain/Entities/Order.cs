using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Status { get; set; }//enum değer yazıcaz
        public List<OrderItem> OrderItems { get; set; } 
    }
}
