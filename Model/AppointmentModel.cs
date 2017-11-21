using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class AppointmentModel: INotifyPropertyChanged
    {
        private Appointment _appointment = new Appointment();
        private String _appointmentColor;

        public DateTime Date 
        { 
            get
            {
                return _appointment.AppointmentDate;
            }
            set
            {
                _appointment.AppointmentDate = value;
                OnPropertyChanged("Date");
            }
        }

        public DateTime Start 
        { 
            get
            {
                return _appointment.StartTime;
            }
            set
            {
                _appointment.StartTime = value;
                OnPropertyChanged("Start");
                OnPropertyChanged("AppointmentText");
            }
        }
        public DateTime End 
        {
            get
            {
                return _appointment.EndTime;
            }
            set
            {
                _appointment.EndTime = value;
                OnPropertyChanged("End");
                OnPropertyChanged("AppointmentText");
            }
        }
        public String Title 
        { 
            get
            {
                return _appointment.Title;
            }
            set
            {
                _appointment.Title = value;
                OnPropertyChanged("Title");
                OnPropertyChanged("AppointmentText");
            }
        }

        public String AppointmentColor
        {
            get
            {
                return _appointmentColor;
            }
            set
            {
                _appointmentColor = value;
                OnPropertyChanged("AppointmentColor");
            }
        }

        public String AppointmentText
        {
            get
            {
                return Start.ToString("HH:mm") + "-" + End.ToString("HH:mm") + " " + Title;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AppointmentModel()
        { }
        
        public AppointmentModel(DateTime date, DateTime start, DateTime end, String title)
        {
            Date = date;
            Start = start;
            End = end;
            Title = title;
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
