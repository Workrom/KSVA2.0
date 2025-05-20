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
    /// Interaction logic for TeacherProfileWindow.xaml
    /// </summary>
    public partial class TeacherProfileWindow : Window
    {
        private user _user;
        private teacher_profile _tp;

        private TeacherProfileService profileservice;
        public TeacherProfileWindow()
        {
            InitializeComponent();

            FillData();
            profileservice = new TeacherProfileService();
        }

        public TeacherProfileWindow(user user, teacher_profile tp)
        {
            InitializeComponent();

            _user = user;
            _tp = tp;
            FillData(user, tp);
            profileservice = new TeacherProfileService();
            HideNIUI();
        }
        public void HideNIUI()
        {
            //txbs
            Nametxb.IsReadOnly = true;
            Passwordtxb.IsReadOnly = true;
            Phonetxb.IsReadOnly = true;
            Dobtxb.IsReadOnly = true;
            Expertisecbx.IsHitTestVisible = false;
            Subjecttxb.IsReadOnly = true;
            Pricetxb.IsReadOnly = true;
            //thick
            Nametxb.BorderThickness = new Thickness(0);
            Passwordtxb.BorderThickness = new Thickness(0);
            Phonetxb.BorderThickness = new Thickness(0);
            Dobtxb.BorderThickness = new Thickness(0);
            Expertisecbx.BorderThickness = new Thickness(0);
            Subjecttxb.BorderThickness = new Thickness(0);
            Pricetxb.BorderThickness = new Thickness(0);
            //just to be sure
            Changebtn.IsEnabled = false;
            Logoutbtn.IsEnabled = false;
            Applybtn.IsEnabled = false;
            Cancelbtn.IsEnabled = false;
            //hide them mf`s >:)
            Changebtn.Visibility = Visibility.Collapsed;
            Logoutbtn.Visibility = Visibility.Collapsed;
            Applybtn.Visibility = Visibility.Collapsed;
            Cancelbtn.Visibility = Visibility.Collapsed;
            Passwordtxb.Visibility = Visibility.Collapsed;
            Passwordlbl.Visibility = Visibility.Collapsed;
        }
        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            //txbs
            Nametxb.IsReadOnly = false;
            Passwordtxb.IsReadOnly = false;
            Phonetxb.IsReadOnly = false;
            Dobtxb.IsReadOnly = false;
            Expertisecbx.IsHitTestVisible = true;
            Subjecttxb.IsReadOnly = false;
            Pricetxb.IsReadOnly = false;
            //thick
            Nametxb.BorderThickness = new Thickness(2);
            Passwordtxb.BorderThickness = new Thickness(2);
            Phonetxb.BorderThickness = new Thickness(2);
            Dobtxb.BorderThickness = new Thickness(2);
            Expertisecbx.BorderThickness = new Thickness(2);
            Subjecttxb.BorderThickness = new Thickness(2);
            Pricetxb.BorderThickness = new Thickness(2);

            Changebtn.IsEnabled = false;
            Logoutbtn.IsEnabled = false;
            Applybtn.IsEnabled = true;
            Cancelbtn.IsEnabled = true;
        }

        private void Applybtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nametxb.Text) ||
                string.IsNullOrWhiteSpace(Phonetxb.Text) ||
                string.IsNullOrWhiteSpace(Dobtxb.Text) ||
                string.IsNullOrWhiteSpace(Subjecttxb.Text) ||
                string.IsNullOrWhiteSpace(Pricetxb.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            profileservice.Change(
                SessionManager.CurrentUser.user_id,
                Nametxb.Text,
                Passwordtxb.Text,
                Phonetxb.Text,
                DateOnly.Parse(Dobtxb.Text),
                Expertisecbx.Text,
                Subjecttxb.Text,
                Convert.ToDecimal(Pricetxb.Text));

            MessageBox.Show("Profile succesfuly modified");

            //txbs
            Nametxb.IsReadOnly = true;
            Passwordtxb.IsReadOnly = true;
            Phonetxb.IsReadOnly = true;
            Dobtxb.IsReadOnly = true;
            Expertisecbx.IsHitTestVisible = false;
            Subjecttxb.IsReadOnly = true;
            Pricetxb.IsReadOnly = true;
            //thick
            Nametxb.BorderThickness = new Thickness(0);
            Passwordtxb.BorderThickness = new Thickness(0);
            Phonetxb.BorderThickness = new Thickness(0);
            Dobtxb.BorderThickness = new Thickness(0);
            Expertisecbx.BorderThickness = new Thickness(0);
            Subjecttxb.BorderThickness = new Thickness(0);
            Pricetxb.BorderThickness= new Thickness(0);

            Changebtn.IsEnabled = true;
            Logoutbtn.IsEnabled = true;
            Applybtn.IsEnabled = false;
            Cancelbtn.IsEnabled = false;

            FillData();
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            //txbs
            Nametxb.IsReadOnly = true;
            Passwordtxb.IsReadOnly = true;
            Phonetxb.IsReadOnly = true;
            Dobtxb.IsReadOnly = true;
            Expertisecbx.IsReadOnly = false;
            Subjecttxb.IsReadOnly = true;
            Pricetxb.IsReadOnly = true;
            //thick
            Nametxb.BorderThickness = new Thickness(0);
            Passwordtxb.BorderThickness= new Thickness(0);
            Phonetxb.BorderThickness = new Thickness(0);
            Dobtxb.BorderThickness = new Thickness(0);
            Expertisecbx.BorderThickness = new Thickness(0);
            Subjecttxb.BorderThickness = new Thickness(0);
            Pricetxb.BorderThickness = new Thickness(0);

            Changebtn.IsEnabled = true;
            Logoutbtn.IsEnabled = true;
            Applybtn.IsEnabled = false;
            Cancelbtn.IsEnabled = false;

            FillData();
        }
        private void FillData()
        {
            Nametxb.Text = SessionManager.CurrentUser.name;
            Passwordtxb.Text = SessionManager.CurrentUser.password;
            Phonetxb.Text = SessionManager.CurrentUser.phone;
            Roletxb.Text = SessionManager.CurrentUser.role;
            Dobtxb.Text = SessionManager.CurrentUser.date_of_birth.ToString();
            string currentExpertise = SessionManager.CurrentUser.teacher_profile.expertise;
            foreach (ComboBoxItem item in Expertisecbx.Items)
            {
                if (item.Content.ToString() == currentExpertise)
                {
                    Expertisecbx.SelectedItem = item;
                    break;
                }
            }
            Subjecttxb.Text = SessionManager.CurrentUser.teacher_profile.subject;
            Pricetxb.Text = SessionManager.CurrentUser.teacher_profile.price.ToString();
        }
        private void FillData(user user, teacher_profile tp)
        {
            Nametxb.Text = user.name;
            Passwordtxb.Text = String.Empty;
            Phonetxb.Text = user.phone;
            Roletxb.Text = user.role;
            Dobtxb.Text = user.date_of_birth.ToString();
            string currentExpertise = tp.expertise;
            foreach (ComboBoxItem item in Expertisecbx.Items)
            {
                if (item.Content.ToString() == currentExpertise)
                {
                    Expertisecbx.SelectedItem = item;
                    break;
                }
            }
            Subjecttxb.Text = tp.subject;
            Pricetxb.Text = tp.price.ToString();
        }

        private void Logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.ClearSession();
            new LoginWIndow().Show();

            this.Close();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Close();
                    break;
                }
            }
        }

        private void Statisticsbtn_Click(object sender, RoutedEventArgs e)
        {    
            if(SessionManager.CurrentUser.role == "student")
            {
                this.Close();
                new StatisticsWindow(_user.user_id).ShowDialog();
            }
            else if (SessionManager.CurrentUser.role == "teacher")
            {
                this.Close();
                new StatisticsWindow(SessionManager.CurrentUser.user_id).ShowDialog();
            }
        }
    }
}
