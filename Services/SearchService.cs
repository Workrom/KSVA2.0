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
    internal class SearchService
    {
        private readonly ApplicationContext _context;
        public int MaxPriceFound;

        public SearchService()
        {
            _context = new ApplicationContext();
            MaxPriceFound = Convert.ToInt32(Math.Round(_context.teacher_profiles.Max(tp => tp.price), 0));
        }
        public user GetUserProfile(int userId)
        {
            var userProfile = _context
                .users
                .FirstOrDefault(tp => tp.user_id == userId);
            return userProfile;
        }
        public teacher_profile GetTeacherProfile(int teacherId)
        {
            var teacherProfile = _context
                .teacher_profiles
                .Include(tp => tp.teacher)
                .FirstOrDefault(tp => tp.teacher_id == teacherId);
            return teacherProfile;
        }
        public List<string> GetAvailableSubjects()
        {
            return _context.teacher_profiles
                .Select(tp => tp.subject)
                .Distinct()
                .OrderBy(s => s)
                .ToList();
        }
        public List<object> SearchTeachers(SearchData data)
        {
            var query = _context.teacher_profiles
                .Include(tp => tp.teacher)
                .AsQueryable();

            if (data.UseName && !string.IsNullOrWhiteSpace(data.Name))
            {
                query = query.Where(tp => tp.teacher.name.Contains(data.Name));
            }

            if (data.UseExpertise && !string.IsNullOrWhiteSpace(data.Expertise))
            {
                query = query.Where(tp => tp.expertise == data.Expertise);
            }

            if (data.UseSubject && !string.IsNullOrWhiteSpace(data.Subject))
            {
                query = query.Where(tp => tp.subject == data.Subject);
            }

            if (data.UsePrice)
            {
                query = query.Where(tp => tp.price <= data.MaxPrice);
            }

            return query.Select(tp => new
            {
                id = tp.teacher_id,
                Name = tp.teacher.name,
                Phone = tp.teacher.phone,
                Expertise = tp.expertise,
                Subject = tp.subject,
                Price = tp.price
            }).ToList<object>();
        }
    }
}
