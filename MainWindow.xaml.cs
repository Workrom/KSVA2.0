using KSVA2._0_WPF.Data;
using KSVA2._0_WPF.Services;
using KSVA2._0_WPF.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KSVA2._0_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Profilebtn_Click(object sender, RoutedEventArgs e)
    {
        if (SessionManager.CurrentUser.role == "student")
        {
            new StudentProfileWindow().Show();
        }
        else if(SessionManager.CurrentUser.role == "teacher")
        {
            new TeacherProfileWindow().Show();
        }
    }
}