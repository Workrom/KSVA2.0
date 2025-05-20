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
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
        }
        public StatisticsWindow(int teacherId)
        {
            InitializeComponent();

            var statsService = new StatisticsService();
            var stats = statsService.GetStatistics(teacherId);

            SubjectText.Text = $"📘 Subject: {stats.Subject}";
            OrdersStatusText.Text = "📦 Orders:\n" + string.Join("\n",
                stats.OrderStatusCounts.Select(kv => $"   {kv.Key}: {kv.Value}"));
            AvgRatingText.Text = $"⭐ Average Rating: {stats.AverageRating:F1}/5";
            OrdersThisMonthText.Text = $"📅 Orders This Month: {stats.OrdersThisMonth}";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
