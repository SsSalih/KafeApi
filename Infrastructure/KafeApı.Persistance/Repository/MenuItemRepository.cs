using KafeApı.Aplication.Interfaces;
using KafeApı.Domain.Entities;
using KafeApı.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApı.Persistance.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId)
        {
            var menuItem = await _context.MenuItems.Where(x => x.CategoryId ==categoryId).ToListAsync();
            return menuItem;
        }
    }
}
