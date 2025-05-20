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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KSVA2._0_WPF.Views
{
    /// <summary>
    /// Interaction logic for OrderWindow_uc.xaml
    /// </summary>
    public partial class OrderWindow_uc : UserControl
    {
        private readonly OrderService orderService;
        private readonly ReviewService reviewService;
        public OrderWindow_uc()
        {
            InitializeComponent();
            orderService = new OrderService();
            reviewService = new ReviewService();
            AdjustUIForRole();
            LoadOrders();
        }
        private void AdjustUIForRole()
        {
            var user = SessionManager.CurrentUser;

            if (user.role == "student")
            {
                Finishbtn.Visibility = Visibility.Visible;
                Cancelbtn.Visibility = Visibility.Visible;

                Acceptbtn.Visibility = Visibility.Collapsed;
                Declinebtn.Visibility = Visibility.Collapsed;
                Latebtn.Visibility = Visibility.Collapsed;
            }
            else if (user.role == "teacher")
            {
                Acceptbtn.Visibility = Visibility.Visible;
                Declinebtn.Visibility = Visibility.Visible;
                Latebtn.Visibility = Visibility.Visible;

                Finishbtn.Visibility = Visibility.Collapsed;
                Cancelbtn.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadOrders()
        {
            Ordersdg.ItemsSource = orderService.GetOrdersForUser(SessionManager.CurrentUser);
        }
        private void Finishbtn_Click(object sender, RoutedEventArgs e)
        {
            var orderId = GetSelectedOrderId();
            var orderStatus = orderService.GetOrderStatus(orderId);
            if (orderId == null || orderStatus is not "ONGOING") return;

            if (orderService.UpdateOrderStatus(orderId.Value, "DONE"))
            {
                var reviewWindow = new ReviewWindow(orderService.GetOrder(orderId.Value));
                reviewWindow.ShowDialog();
                LoadOrders();
            }
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            var orderId = GetSelectedOrderId();
            var orderStatus = orderService.GetOrderStatus(orderId);
            if (orderId == null || orderStatus is not "AWAIT") return;

            if (orderService.CancelOrder(orderId.Value))
            {
                MessageBox.Show("Order declined.");
                LoadOrders();
            }
        }

        private void Acceptbtn_Click(object sender, RoutedEventArgs e)
        {
            var orderId = GetSelectedOrderId();
            var orderStatus = orderService.GetOrderStatus(orderId);
            if (orderId == null || orderStatus is not "AWAIT") return;

            if (orderService.UpdateOrderStatus(orderId.Value, "ONGOING"))
            {
                MessageBox.Show("Order accepted.");
                LoadOrders();
            }
        }

        private void Declinebtn_Click(object sender, RoutedEventArgs e)
        {
            var orderId = GetSelectedOrderId();
            var orderStatus = orderService.GetOrderStatus(orderId);
            if (orderId == null || orderStatus is "DONE") return;

            if (orderService.CancelOrder(orderId.Value))
            {
                MessageBox.Show("Order declined.");
                LoadOrders();
            }
        }

        private int? GetSelectedOrderId()
        {
            var selected = Ordersdg.SelectedItem;
            if (selected == null) return null;

            var idProp = selected.GetType().GetProperty("id");
            if (idProp == null) return null;

            return idProp.GetValue(selected) as int?;
        }

        private void Ordersdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var orderId = GetSelectedOrderId();

            if (orderId == null)
            {
                Reviewtxb.Text = "";
                Reviewscorelbl.Content = "0/5";
                return;
            }
            var review = reviewService.GetReviewByOrderId(orderId.Value);

            if (review != null)
            {
                Reviewtxb.Text = review.review_text;
                Reviewscorelbl.Content = $"{review.rating}/5";
            }
            else
            {
                Reviewtxb.Text = "No review yet.";
                Reviewscorelbl.Content = "0/5";
            }
        }

        private void Latebtn_Click(object sender, RoutedEventArgs e)
        {
            var orderId = GetSelectedOrderId();
            var orderStatus = orderService.GetOrderStatus(orderId);
            if (orderId == null || orderStatus is not "AWAIT") return;

            if (orderService.UpdateOrderStatus(orderId.Value, "LATE"))
            {
                MessageBox.Show("Order succesfuly marked as Late.");
                LoadOrders();
            }
        }
    }
}
