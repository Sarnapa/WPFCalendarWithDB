using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
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
            GetSortedDayAppointmentsList();
        }

        public void AddAppointment(AppointmentModel e)
        {
            e.SaveNewAppointmentInDB(_userID);
            GetSortedDayAppointmentsList();
        }

        public void ModifyAppointment(AppointmentModel e)
        {
            try
            {
                e.SaveModifiedAppointmentInDB();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GetSortedDayAppointmentsList();
            }
        }

        public void RemoveAppointment(AppointmentModel e)
        {
            try
            {
                e.RemoveAppointmentInDB();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                GetSortedDayAppointmentsList();
            }
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

        private void GetSortedDayAppointmentsList()
        {
            AppointmentsList = GetDayAppointments();
            AppointmentsList = new ObservableCollection<AppointmentModel>(_appointmentsList.OrderBy(o => o.Start));
            if(AppointmentsList.Count > 0)
                Logger.Log.Info("Get " + AppointmentsList.Count + " appointments from DB for date: " + _date.ToString("dd.MM.yyyy"));
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
