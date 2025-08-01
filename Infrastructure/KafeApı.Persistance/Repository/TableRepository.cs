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
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Table>> GetAllActiveTablesAsync()
        {
            var tables = await _context.Tables.Where(x =>x.IsActive == true).ToListAsync();
            return tables;
        }

        public async Task<Table> GetByableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables.FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;
        }
    }
}
