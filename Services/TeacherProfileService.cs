using KSVA2._0_WPF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KSVA2._0_WPF.Services
{
    internal class TeacherProfileService
    {
        private readonly ApplicationContext _context;

        public TeacherProfileService()
        {
            _context = new ApplicationContext();
        }

        public void Change(int userid, string new_name, string new_phone, DateOnly new_dateofbirth, string expertise, string subject, decimal price)
        {
            var user = _context.users.Include(u => u.teacher_profile).FirstOrDefault(u => u.user_id == userid);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            user.name = new_name;
            user.phone = new_phone;
            user.date_of_birth = new_dateofbirth;
            user.teacher_profile.expertise = expertise;
            user.teacher_profile.subject = subject;
            user.teacher_profile.price = price;

            _context.SaveChanges();

            SessionManager.InitializeSession(user);
        }
    }
}
