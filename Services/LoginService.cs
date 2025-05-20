using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Models;
using Microsoft.EntityFrameworkCore;
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
            var user = _context.users.Include(u => u.teacher_profile).FirstOrDefault(u => u.name == username);
            if (user != null && password == user.password)
            {
                SessionManager.InitializeSession(user);

                return user;
            }
            return null;
        }
    }
}
