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
    /// Interaction logic for DateSelectionDialogWindow.xaml
    /// </summary>
    public partial class DateSelectionDialogWindow : Window
    {
        public DateTime? SelectedDate => DatePickerControl.SelectedDate;
        public DateSelectionDialogWindow()
        {
            InitializeComponent();
            DatePickerControl.SelectedDate = DateTime.Now;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
