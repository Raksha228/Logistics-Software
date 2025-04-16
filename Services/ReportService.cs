using Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Logistics_Software.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCompletedOrdersCountAsync() =>
            await _context.Orders.CountAsync(o => o.Status == "Выполнен");

        public async Task<Dictionary<string, int>> GetOrdersByStatusAsync()
        {
            return await _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Key, g => g.Count);
        }
    }
}
