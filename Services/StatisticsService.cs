using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSVA2._0_WPF.Services
{
    public class StatisticsService
    {
        private readonly ApplicationContext _context;

        public StatisticsService()
        {
            _context = new ApplicationContext();
        }

        public TeacherStatistics GetStatistics(int teacherId)
        {
            var orders = _context.orders
                .Where(o => o.teacher_id == teacherId)
                .ToList();

            var reviews = _context.reviews
                .Where(r => r.teacher_id == teacherId)
                .ToList();

            var statusCounts = orders
                .GroupBy(o => o.status)
                .ToDictionary(g => g.Key, g => g.Count());

            var avgRating = reviews.Any() ? reviews.Average(r => r.rating) : 0;

            var ordersThisMonth = orders
                .Count(o => o.scheduled_date.Month == DateTime.Now.Month && o.scheduled_date.Year == DateTime.Now.Year);

            var subject = _context.teacher_profiles
                .FirstOrDefault(t => t.teacher_id == teacherId)?.subject ?? "Unknown";

            return new TeacherStatistics
            {
                OrderStatusCounts = statusCounts,
                AverageRating = avgRating,
                OrdersThisMonth = ordersThisMonth,
                Subject = subject
            };
        }
    }

    public class TeacherStatistics
    {
        public Dictionary<string, int> OrderStatusCounts { get; set; } = new();
        public double AverageRating { get; set; }
        public int OrdersThisMonth { get; set; }
        public string Subject { get; set; } = "";
    }
}
