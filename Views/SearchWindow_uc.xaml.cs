using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Models;
using KSVA2._0_WPF.Services;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for SearchWindow_uc.xaml
    /// </summary>
    public partial class SearchWindow_uc : UserControl
    {
        private readonly SearchService searchservice;
        private readonly OrderService orderservice;
        public SearchWindow_uc()
        {
            InitializeComponent();
            searchservice = new SearchService();
            orderservice = new OrderService();
            List<string> subjects = searchservice.GetAvailableSubjects();
            Pricesdr.Maximum = searchservice.MaxPriceFound;

            Subjectcmx.Items.Clear();
            foreach (var subject in subjects)
            {
                Subjectcmx.Items.Add(subject);
            }
        }
        private void Profilebtn_Click(object sender, RoutedEventArgs e)
        {
            if (SessionManager.CurrentUser.role == "student")
            {
                new StudentProfileWindow().Show();
            }
            else if (SessionManager.CurrentUser.role == "teacher")
            {
                new TeacherProfileWindow().Show();
            }
        }
        private void Searchllbl_Click(object sender, RoutedEventArgs e)
        {
            var data = new SearchData
            {
                UseName = Namecbx.IsChecked == true,
                Name = Nametxb.Text,

                UseExpertise = Expertisecbx.IsChecked == true,
                Expertise = (Expertisecmx.SelectedItem as ComboBoxItem)?.Content?.ToString(),

                UseSubject = Subjectcbx.IsChecked == true,
                Subject = Subjectcmx.SelectedItem?.ToString(),

                UsePrice = Pricecbx.IsChecked == true,
                MaxPrice = (decimal)Pricesdr.Value
            };

            var result = searchservice.SearchTeachers(data);

            Teachersdg.ItemsSource = result;
        }

        private void FilterCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is string targetName)
            {
                var target = this.FindName(targetName) as Control;
                if (target != null)
                    target.IsEnabled = true;
            }
        }

        private void FilterCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is string targetName)
            {
                var target = this.FindName(targetName) as Control;
                if (target != null)
                    target.IsEnabled = false;
            }
        }
        private void Orderbtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = Teachersdg.SelectedItem;
            var currentUser = SessionManager.CurrentUser;

            if (selectedRow == null || currentUser == null)
            {
                MessageBox.Show("Missing data.");
                return;
            }

            var teacherIdProperty = selectedRow.GetType().GetProperty("id");
            if (teacherIdProperty == null)
            {
                MessageBox.Show("Could not extract teacher ID.");
                return;
            }

            int teacherId = (int)teacherIdProperty.GetValue(selectedRow);
            teacher_profile teacherProfile = searchservice.GetTeacherProfile(teacherId);

            if (teacherProfile == null)
            {
                MessageBox.Show("Teacher profile not found.");
                return;
            }

            var parentWindow = Window.GetWindow(this);
            var dateDialog = new DateSelectionDialogWindow
            {
                Owner = parentWindow
            };

            if (dateDialog.ShowDialog() == true)
            {
                var selectedDate = dateDialog.SelectedDate ?? DateTime.Now;

                if (orderservice.PlaceOrder(currentUser.user_id, teacherProfile, selectedDate))
                {
                    MessageBox.Show("Order placed for " + selectedDate.ToShortDateString());
                }
                else
                {
                    MessageBox.Show("Order failed.");
                }
            }
        }

        private void ProfileLbtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = Teachersdg.SelectedItem;
            var teacherIdProperty = selectedRow.GetType().GetProperty("id");
            if (teacherIdProperty == null)
            {
                MessageBox.Show("Could not extract teacher ID.");
                return;
            }

            int teacherId = (int)teacherIdProperty.GetValue(selectedRow);
            
            user userProfile = searchservice.GetUserProfile(teacherId);
            teacher_profile teacherProfile = searchservice.GetTeacherProfile(teacherId);

            if (teacherProfile == null)
            {
                MessageBox.Show("Teacher profile not found.");
                return;
            }

            new TeacherProfileWindow(userProfile, teacherProfile).Show();
        }
    }
}
