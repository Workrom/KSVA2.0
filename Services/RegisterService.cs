using KSVA2._0_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSVA2._0_WPF.Services
{
    internal class RegisterService
    {
        private readonly ApplicationContext _context;
        public RegisterService()
        {
            _context = new ApplicationContext();
        }

        public bool Register(string username, string password, string phone, DateOnly dob, string role, string? subject = null, decimal? price = null, string? expertise = null)
        {
            var user = new user
            {
                name = username,
                password = PassHashService.Hash(password),
                phone = phone,
                date_of_birth = dob,
                role = role
            };

            _context.users.Add(user);
            _context.SaveChanges();

            if (role == "teacher")
            {
                var profile = new teacher_profile
                {
                    teacher_id = user.user_id,
                    subject = subject!,
                    price = price!.Value,
                    expertise = expertise!
                };

                _context.teacher_profiles.Add(profile);
                _context.SaveChanges();
            }

            return true;
        }
    }

}
