using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Models;
using KSVA2._0_WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KSVA2._0_WPF.Views
{
    /// <summary>
    /// Interaction logic for ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : Window
    {
        private readonly ReviewService _reviewService = new();
        private readonly order _order;
        public ReviewWindow()
        {
            InitializeComponent();
        }   

        public ReviewWindow(order order)
        {
            InitializeComponent();
            _order = order;
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.CurrentUser;
            if (user == null) return;

            var rating = (sbyte)RatingSlider.Value;
            var text = ReviewText.Text;

            var success = _reviewService.SubmitReview(user.user_id, _order.teacher_id, _order.order_id, text, rating);

            if (success)
            {
                MessageBox.Show("Review submitted.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to submit review.");
            }
        }
    }
}
