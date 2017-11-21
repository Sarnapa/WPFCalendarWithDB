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

        public Day(DateTime date, String dateColor, String appointmentColor, String mainFontColor)
        {
            Date = date;
            DateColor = dateColor;
            _appointmentColor = appointmentColor;
            MainFontColor = mainFontColor;
            AppointmentsList = new ObservableCollection<AppointmentModel>();
        }

        public void AddAppointment(AppointmentModel e)
        {
            e.AppointmentColor = _appointmentColor;
            _appointmentsList.Add(e);
            AppointmentsList = new ObservableCollection<AppointmentModel>(_appointmentsList.OrderBy(o => o.Start));
        }

        public void RemoveAppointment(AppointmentModel e)
        {
            _appointmentsList.Remove(e);
        }

        /*
        private ObservableCollection<EventModel> GetDayEvents(List<EventModel> allEventsList)
        {
            ObservableCollection<EventModel> eventsList = new ObservableCollection<EventModel>();
            if (allEventsList != null)
            {
                foreach (EventModel e in allEventsList)
                {
                    // Date.Date - "pozbywamy się" godziny na wszelki wypadek
                    if (e.Date.Date.CompareTo(Date.Date) == 0)
                    {
                        e.EventColor = _eventColor;
                        eventsList.Add(e);
                    }
                }
            }
            eventsList = new ObservableCollection<EventModel>(eventsList.OrderBy(o => o.Start));
            return eventsList;
        }*/

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
