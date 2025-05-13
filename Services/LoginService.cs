using KSVA2._0_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSVA2._0_WPF.Services
{
    internal class LoginService
    {
        private readonly ApplicationContext _context;

        public LoginService()
        {
            _context = new ApplicationContext();
        }

        public user? Login(string username, string password)
        {
            return _context.users
                .FirstOrDefault(u => u.name == username && u.password == password);
        }
    }
}
