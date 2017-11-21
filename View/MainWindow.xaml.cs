using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WPFCalendarWithDB.Model;
using WPFCalendarWithDB.ViewModel;

namespace WPFCalendarWithDB.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoubleClickAppointmentHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                MainViewModel mainVM = this.mainViewModel;
                EditWindow editWindow = new EditWindow();
                EditViewModel editVM = editWindow.editViewModel;
                Day currentDay = (Day)((StackPanel)sender).DataContext;
                if (e.OriginalSource is StackPanel)
                {
                    editWindow.removeAppointmentButton.Visibility = System.Windows.Visibility.Hidden;
                    editWindow.addModifyAppointmentButton.Content = "Add appointment";
                    Boolean? res = editWindow.ShowDialog();
                    if (res.HasValue && res.Value)
                    {
                        editVM.AddNewAppointment(currentDay.Date);
                        mainVM.AddAppointment(currentDay, editVM.Appointment);
                    }
                }
                else if (e.OriginalSource is TextBlock)
                {
                    var appointmentTextBox = ((TextBlock)e.OriginalSource);
                    if (appointmentTextBox.Name == "appointmentTextBlock")
                    {
                        editWindow.removeAppointmentButton.Visibility = System.Windows.Visibility.Visible;
                        editWindow.addModifyAppointmentButton.Content = "Modify appointment";
                        var currentAppointment = (AppointmentModel)appointmentTextBox.DataContext;
                        editVM.Appointment = currentAppointment;
                        Boolean? res = editWindow.ShowDialog();
                        if (res.HasValue && res.Value)
                        {
                            if (editVM.IsRemoveAppointment)
                            {
                                mainVM.RemoveAppointment(currentDay, editVM.Appointment);
                            }
                            else
                            {
                                editVM.ModifyAppointment();
                                mainVM.ModifyAppointment(currentDay);
                            }
                        }
                    }
                    else
                    {
                        editWindow.removeAppointmentButton.Visibility = System.Windows.Visibility.Hidden;
                        editWindow.addModifyAppointmentButton.Content = "Add appointment";
                        Boolean? res = editWindow.ShowDialog();
                        if (res.HasValue && res.Value)
                        {
                            editVM.AddNewAppointment(currentDay.Date);
                            mainVM.AddAppointment(currentDay, editVM.Appointment);
                        }
                    }
                }

            }
        }
    }
}