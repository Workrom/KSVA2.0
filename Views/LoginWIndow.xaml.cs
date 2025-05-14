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
    /// Interaction logic for LoginWIndow.xaml
    /// </summary>
    public partial class LoginWIndow : Window
    {
        public LoginWIndow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var service = new LoginService();
            var user = service.Login(Usernametxb.Text, Passwordtxb.Password);

            if (user != null)
            {
                MessageBox.Show($"Welcome, {user.name}!");
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var RegisterWindow = new RegisterWindow();
            RegisterWindow.Show();
            this.Close();
        }
    }
}
