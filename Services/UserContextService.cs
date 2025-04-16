using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.Services
{
    public class UserContextService
    {
        public User? CurrentUser { get; private set; }

        public void SetUser(User user) => CurrentUser = user;
        public void Logout() => CurrentUser = null;

        public bool IsInRole(string role) => CurrentUser?.Role == role;
    }
}
