using Backend.DataAccess;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logistics_Software.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // Получить все заказы
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Manager)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        // Получить заказы по пользователю
        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.ClientId == userId)
                .Include(o => o.Client)
                .Include(o => o.Manager)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        // Создание нового заказа
        public async Task<Order?> CreateOrderAsync(int clientId, string description)
        {
            var user = await _context.Users.FindAsync(clientId);
            if (user == null || user.Role != "Client")
                return null;

            var order = new Order
            {
                ClientId = clientId,
                Description = description,
                Status = "Создан",
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        // Обновление статуса заказа
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        // Назначить менеджера к заказу
        public async Task<bool> AssignManagerAsync(int orderId, int managerId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            var manager = await _context.Users.FindAsync(managerId);

            if (order == null || manager == null || manager.Role != "Manager")
                return false;

            order.ManagerId = managerId;
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        // Удаление заказа
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        // Получить заказ по ID
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Manager)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
