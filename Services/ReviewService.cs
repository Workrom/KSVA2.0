using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSVA2._0_WPF.Models;

namespace KSVA2._0_WPF.Services
{
    public class ReviewService
    {
        private readonly ApplicationContext _context;

        public ReviewService()
        {
            _context = new ApplicationContext();
        }
        public review? GetReviewByOrderId(int orderId)
        {
            return _context.reviews.FirstOrDefault(r => r.order_id == orderId);
        }

        public bool SubmitReview(int studentId, int teacherId, int orderId, string reviewText, sbyte rating)
        {
            var review = new review
            {
                student_id = studentId,
                teacher_id = teacherId,
                order_id = orderId,
                review_text = reviewText,
                rating = rating,
                date = DateTime.Now
            };

            _context.reviews.Add(review);
            _context.SaveChanges();
            return true;
        }
    }

}
