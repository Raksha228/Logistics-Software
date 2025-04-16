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
    public class AuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly UserContextService _userContext;

        public AuthenticationService(AppDbContext context, UserContextService userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password); // TODO: Хешировать пароль

            if (user != null)
            {
                _userContext.SetUser(user);
                return true;
            }

            return false;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                return false;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
