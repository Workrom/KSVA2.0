using KSVA2._0_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSVA2._0_WPF.Services
{
    public class OrderService
    {
        private readonly ApplicationContext _context;

        public OrderService()
        {
            _context = new ApplicationContext();
        }
        public order GetOrder(int orderid)
        {
            return _context.orders.Find(orderid);
        }
        public string GetOrderStatus(int? orderid)
        {
            var order = _context.orders.Find(orderid);
            if (order == null) return null;
            return order.status;
        }
        public bool PlaceOrder(int studentId, teacher_profile teacher, DateTime scheduledDate)
        {
            if (teacher == null) return false;

            var newOrder = new order
            {
                teacher_id = teacher.teacher_id,
                student_id = studentId,
                subject = teacher.subject,
                scheduled_date = scheduledDate,
                status = "AWAIT"
            };

            _context.orders.Add(newOrder);
            _context.SaveChanges();
            return true;
        }

        public List<object> GetOrdersForUser(user currentUser)
        {
            if (currentUser == null) return new List<object>();

            var ordersQuery = _context.orders
                .Include(o => o.teacher)
                .Include(o => o.student)
                .AsQueryable();

            if (currentUser.role == "student")
            {
                ordersQuery = ordersQuery.Where(o => o.student_id == currentUser.user_id);
            }
            else if (currentUser.role == "teacher")
            {
                ordersQuery = ordersQuery.Where(o => o.teacher_id == currentUser.user_id);
            }

            return ordersQuery
                .Select(o => new
                {
                    id = o.order_id,
                    Student = o.student.name,
                    Teacher = o.teacher.teacher.name,
                    Subject = o.subject,
                    ScheduledDate = o.scheduled_date,
                    Status = o.status
                })
                .ToList<object>();
        }

        public bool UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _context.orders.Find(orderId);
            if (order == null) return false;

            order.status = newStatus;
            _context.SaveChanges();
            return true;
        }

        public bool CancelOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            if (order == null) return false;

            _context.orders.Remove(order);
            _context.SaveChanges();
            return true;
        }
    }

}
