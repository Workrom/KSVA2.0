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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private RegisterService regservice;
        public RegisterWindow()
        {
            InitializeComponent();
            Expertisecbx.IsEnabled = false;
            Subjecttxb.IsEnabled = false;
            Pricetxb.IsEnabled = false;
        }

        private void Teachercbx_Checked(object sender, RoutedEventArgs e)
        {
            Expertisecbx.IsEnabled = true;
            Subjecttxb.IsEnabled = true;
            Pricetxb.IsEnabled = true;

        }

        private void Teachercbx_Unchecked(object sender, RoutedEventArgs e)
        {
            Expertisecbx.IsEnabled = false;
            Subjecttxb.IsEnabled = false;
            Pricetxb.IsEnabled = false;
        }

        private void Registerbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string role = Teachercbx.IsChecked == true ? "teacher" : "student";
                string username = Usernametxb.Text.Trim();
                string password = Passwordtxb.Text.Trim();
                string phone = Phonetxb.Text.Trim();
                if (!DateOnly.TryParse(Birthdtp.SelectedDate.ToString(), out DateOnly dob))
                    return;

                string? subject = null;
                decimal? price = null;
                string? expertise = null;

                if (role == "teacher")
                {
                    subject = Subjecttxb.Text.Trim();
                    expertise = ((ComboBoxItem)Expertisecbx.SelectedItem)?.Content?.ToString() ?? "high school";
                    price = decimal.TryParse(Pricetxb.Text, out var parsedPrice) ? parsedPrice : 0;
                }

                var service = new RegisterService();
                bool success = service.Register(username,password,phone,dob,role,subject,price,expertise
                );

                if (success)
                {
                    MessageBox.Show("Registered successfully!");
                    var loginwindow = new LoginWIndow();
                    loginwindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
