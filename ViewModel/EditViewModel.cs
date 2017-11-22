using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFCalendarWithDB.Model;

namespace WPFCalendarWithDB.ViewModel
{
    public class EditViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private AppointmentModel _appointment;
        private Boolean _isRemoveAppointment;
        private String _appointmentStart;
        private String _appointmentEnd;
        private String _appointmentTitle;
        private String _appointmentDescription;
        private ICommand _removeAppointmentCommand;
        private ICommand _closeCommand;

        public String Error
        {
            get { return String.Empty; }
        }

        public String this[string fieldName]
        {
            get
            {
                String result = null;
                DateTime tempDate = new DateTime();
                String timeFormat = "HH:mm";
                switch (fieldName)
                {
                    case "AppointmentTitle":
                        if (String.IsNullOrEmpty(AppointmentTitle))
                            result = "Title field must not be empty!";
                        else if (AppointmentTitle.Length > 16)
                            result = "Title must be at most 16 characters in length!";
                        break;
                    case "AppointmentStart":
                        if (String.IsNullOrEmpty(AppointmentStart))
                            result = "Start time field must not be empty!";
                        else if (!DateTime.TryParseExact(AppointmentStart, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
                            result = "Required format: HH:mm";
                        break;
                    case "AppointmentEnd":
                        if (String.IsNullOrEmpty(AppointmentEnd))
                            result = "End time field must not be empty!";
                        else if (!DateTime.TryParseExact(AppointmentEnd, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
                            result = "Required format: HH:mm";
                        break;
                    case "AppointmentDescription":
                        if (String.IsNullOrEmpty(AppointmentDescription))
                            result = "Title field must not be empty!";
                        else if (AppointmentDescription.Length > 50)
                            result = "Title must be at most 50 characters in length!";
                        break;
                }
                return result;
            }
        }

         public AppointmentModel Appointment
        {
            get
            {
                return _appointment;
            }
            set
            {
                _appointment = value;
                AppointmentTitle = _appointment.Title;
                AppointmentStart = _appointment.Start.ToString("HH:mm");
                AppointmentEnd = _appointment.End.ToString("HH:mm");
                AppointmentDescription = _appointment.Description;
            }
        }

        public Boolean IsRemoveAppointment
        {
            get
            {
                return _isRemoveAppointment;
            }
            set
            {
                _isRemoveAppointment = value;
            }
        }


        public String AppointmentStart
        {
            get
            {
                return _appointmentStart;
            }
            set
            {
                _appointmentStart = value;
                OnPropertyChanged("AppointmentStart");
            }
        }

        public String AppointmentEnd
        {
            get
            {
                return _appointmentEnd;
            }
            set
            {
                _appointmentEnd = value;
                OnPropertyChanged("AppointmentEnd");
            }
        }

        public String AppointmentTitle
        {
            get
            {
                return _appointmentTitle;
            }
            set
            {
                _appointmentTitle = value;
                OnPropertyChanged("AppointmentTitle");
            }
        }

        public String AppointmentDescription
        {
            get
            {
                return _appointmentDescription;
            }
            set
            {
                _appointmentDescription = value;
                OnPropertyChanged("AppointmentDescription");
            }
        }

        public ICommand RemoveAppointmentCommand
        {
            get
            {
                return _removeAppointmentCommand;
            }
            set
            {
                _removeAppointmentCommand = value;
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand;
            }
            set
            {
                _closeCommand = value;
            }
        }

        public Action CloseAction { get; set; }
        public Action RemoveAppointmentAction { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public EditViewModel()
        {
            CloseCommand = new RelayCommand(
                new Action<object>(
                    delegate(object obj)
                    {
                        CloseAction();
                    }
                    ), null);

            RemoveAppointmentCommand = new RelayCommand(
                 new Action<object>(
                     delegate(object obj)
                     {
                         RemoveAppointmentAction();
                     }
                     ), null);
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void AddNewAppointment(DateTime date)
        {
            _appointment = new AppointmentModel();
            _appointment.Date = date;
            DateTime startTime = GetTimeFromString(_appointmentStart);
            DateTime endTime = GetTimeFromString(_appointmentEnd);
            _appointment.Title = _appointmentTitle;
            _appointment.Start = startTime;
            _appointment.End = endTime;
            _appointment.Description = _appointmentDescription;
        }

        public void ModifyAppointment()
        {
            DateTime startTime = GetTimeFromString(_appointmentStart);
            DateTime endTime = GetTimeFromString(_appointmentEnd);
            _appointment.Title = _appointmentTitle;
            _appointment.Start = startTime;
            _appointment.End = endTime;
            _appointment.Description = _appointmentDescription;
        }

        private DateTime GetTimeFromString(String timeString)
        {
            int hour = Int32.Parse(timeString.Substring(0, 2));
            int minutes = Int32.Parse(timeString.Substring(3, 2));
            return new DateTime(_appointment.Date.Year, _appointment.Date.Month, 
                _appointment.Date.Day, hour, minutes, 0);
        }
    }
}
