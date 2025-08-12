using KafeApı.Aplication.Interfaces;
using KafeApı.Domain.Entities;
using KafeApı.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Persistance.Repository
{
    public class OrderReposiory : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderReposiory(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Order>> GetAllOrderWidthDetailAsync()
        {
            var result = await _appDbContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.MenuItem).ThenInclude(x=> x.Category).ToListAsync();
            return result;
        }

        public async Task<Order> GetOrderByIdWidthDetailAsync(int orderId)
        {
            var result = await _appDbContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.MenuItem).ThenInclude(x => x.Category).Where(x => x.Id ==orderId).FirstOrDefaultAsync();
            return result;
        }
    }
}
