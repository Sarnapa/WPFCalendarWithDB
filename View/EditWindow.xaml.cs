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

namespace WPFCalendarWithDB.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            this.editViewModel.CloseAction = new Action(() =>
            {
                this.DialogResult = true;
                this.Close();
            });

            this.editViewModel.RemoveAppointmentAction = new Action(() =>
            {
                this.DialogResult = true;
                this.editViewModel.IsRemoveAppointment = true;
                this.Close();
            });
        }
    }
}
