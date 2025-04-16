using Backend.DataAccess;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logistics_Software.Services
{
    class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync() =>
            await _context.Orders.Include(o => o.Client).ToListAsync();

        public async Task<List<Order>> GetUserOrdersAsync(int userId) =>
            await _context.Orders.Where(o => o.ClientId == userId).ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(int id) =>
            await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
