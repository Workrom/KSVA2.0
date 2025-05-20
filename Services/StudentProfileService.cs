using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KSVA2._0_WPF.Services
{
    internal class StudentProfileService
    {
        private readonly ApplicationContext _context;

        public StudentProfileService()
        {
            _context = new ApplicationContext();
        }
        public void Change(int userid, string new_name, string new_password, string new_phone, DateOnly new_dateofbirth)
        {
            var user = _context.users.FirstOrDefault(u => u.user_id == userid);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            user.name = new_name;
            user.password = new_password;
            user.phone = new_phone;
            user.date_of_birth = new_dateofbirth;

            _context.SaveChanges();

            SessionManager.InitializeSession(user);
        }
    }
}
