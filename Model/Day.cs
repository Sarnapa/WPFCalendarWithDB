using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Day: INotifyPropertyChanged
    {
        private DateTime _date;
        private ObservableCollection<AppointmentModel> _appointmentsList;
        private String _dateColor;
        private String _appointmentColor;
        private String _mainFontColor;
        private String _userID;
        private Storage _storage = new Storage();

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public ObservableCollection<AppointmentModel> AppointmentsList
        {
            get
            {
                return _appointmentsList;
            }
            set
            {
                _appointmentsList = value;
                OnPropertyChanged("AppointmentsList");
            }
        }

        public String DateColor
        {
            get
            {
                return _dateColor;
            }
            set
            {
                _dateColor = value;
                OnPropertyChanged("DateColor");
            }
        }

        public String MainFontColor
        {
            get
            {
                return _mainFontColor;
            }
            set
            {
                _mainFontColor = value;
                OnPropertyChanged("MainFontColor");
            }
        }


        public String DateText
        {
            get
            {
                return _date.ToString("MMMM dd", CultureInfo.CreateSpecificCulture("en-GB")); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Day(DateTime date, String dateColor, String appointmentColor, String mainFontColor, String userID)
        {
            Date = date;
            DateColor = dateColor;
            _appointmentColor = appointmentColor;
            MainFontColor = mainFontColor;
            _userID = userID;
            AppointmentsList = GetDayAppointments();
        }

        public void AddAppointment(AppointmentModel e)
        {
            e.AppointmentColor = _appointmentColor;
            _appointmentsList.Add(e);
            e.SaveNewAppointmentInDB(_userID);
            AppointmentsList = new ObservableCollection<AppointmentModel>(_appointmentsList.OrderBy(o => o.Start));
        }

        public void ModifyAppointment(AppointmentModel e)
        {
            e.SaveModifiedAppointmentInDB();
            AppointmentsList = new ObservableCollection<AppointmentModel>(_appointmentsList.OrderBy(o => o.Start));
        }

        public void RemoveAppointment(AppointmentModel e)
        {
            e.RemoveAppointmentInDB();
            _appointmentsList.Remove(e);
        }

        private ObservableCollection<AppointmentModel> GetDayAppointments()
        {
            ObservableCollection<AppointmentModel> result = new ObservableCollection<AppointmentModel>();
            List<Appointment> appointmentsDBList = _storage.GetDayAppointments(_userID, _date);
            foreach(Appointment appointment in appointmentsDBList)
            {
                AppointmentModel newAppointmentModel = new AppointmentModel(appointment);
                newAppointmentModel.AppointmentColor = _appointmentColor;
                result.Add(newAppointmentModel);
            }
            result = new ObservableCollection<AppointmentModel>(result.OrderBy(o => o.Start));
            return result;
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
