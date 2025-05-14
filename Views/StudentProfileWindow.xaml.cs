using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml;
using System.Xml.Linq;

namespace KSVA2._0_WPF.Views
{
    /// <summary>
    /// Interaction logic for StudentProfileWindow.xaml
    /// </summary>
    public partial class StudentProfileWindow : Window
    {
        private StudentProfileService profileservice;
        public StudentProfileWindow()
        {
            InitializeComponent();

            FillData();
            profileservice = new StudentProfileService();
        }
        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            //txbs
            Nametxb.IsReadOnly = false;
            Phonetxb.IsReadOnly = false;
            Dobtxb.IsReadOnly = false;
            //thick
            Nametxb.BorderThickness = new Thickness(2);
            Phonetxb.BorderThickness = new Thickness(2);
            Dobtxb.BorderThickness = new Thickness(2); 


            Changebtn.IsEnabled = false;
            Applybtn.IsEnabled = true;
            Cancelbtn.IsEnabled = true;
        }

        private void Applybtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nametxb.Text) ||
               string.IsNullOrWhiteSpace(Phonetxb.Text) ||
               string.IsNullOrWhiteSpace(Dobtxb.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            profileservice.Change(
                SessionManager.CurrentUser.user_id,
                Nametxb.Text,
                Phonetxb.Text,
                DateOnly.Parse(Dobtxb.Text));

            MessageBox.Show("Profile succesfuly modified");

            //txbs
            Nametxb.IsReadOnly = true;
            Phonetxb.IsReadOnly = true;
            Dobtxb.IsReadOnly = true;
            //thick
            Nametxb.BorderThickness = new Thickness(0);
            Phonetxb.BorderThickness = new Thickness(0);
            Dobtxb.BorderThickness = new Thickness(0);


            Changebtn.IsEnabled = true;
            Applybtn.IsEnabled = false;
            Cancelbtn.IsEnabled = false;

            FillData();
        }
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            //txbs
            Nametxb.IsReadOnly = true;
            Phonetxb.IsReadOnly = true;
            Dobtxb.IsReadOnly = true;
            //thick
            Nametxb.BorderThickness = new Thickness(0);
            Phonetxb.BorderThickness = new Thickness(0);
            Dobtxb.BorderThickness = new Thickness(0);


            Changebtn.IsEnabled = true;
            Applybtn.IsEnabled = false;
            Cancelbtn.IsEnabled = false;

            FillData();
        }

        private void FillData()
        {
            Nametxb.Text = SessionManager.CurrentUser.name;
            Phonetxb.Text = SessionManager.CurrentUser.phone;
            Roletxb.Text = SessionManager.CurrentUser.role;
            Dobtxb.Text = SessionManager.CurrentUser.date_of_birth.ToString();
        }
    }
}
