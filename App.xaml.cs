using KSVA2._0_WPF.Services;
using KSVA2._0_WPF.Views;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KSVA2._0_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ApplicationContext _context = new ApplicationContext();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            _context.Database.OpenConnection();
            _context.Database.CloseConnection();

            MessageBox.Show("Connected to MySQL database!", "Connection Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connection failed:\n{ex.Message}", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }
}

