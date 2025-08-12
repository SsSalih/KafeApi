using KafeApı.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Aplication.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderWidthDetailAsync();
        Task<Order> GetOrderByIdWidthDetailAsync(int orderId);
    }
}
