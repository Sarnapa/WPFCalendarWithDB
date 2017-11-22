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
                return ConvertToDateTime(_appointment.StartTime);
            }
            set
            {
                _appointment.StartTime = ConvertToTimeSpan(value);
                OnPropertyChanged("Start");
                OnPropertyChanged("AppointmentText");
            }
        }
        public DateTime End 
        {
            get
            {
                return ConvertToDateTime(_appointment.EndTime);
            }
            set
            {
                _appointment.EndTime = ConvertToTimeSpan(value);
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
        public String Description
        {
            get
            {
                return _appointment.Description;
            }
            set
            {
                _appointment.Description = value;
                OnPropertyChanged("Description");
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
        
        public AppointmentModel(DateTime date, DateTime start, DateTime end, String title, String description)
        {
            Date = date;
            Start = start;
            End = end;
            Title = title;
            Description = description;
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private DateTime ConvertToDateTime(TimeSpan time)
        {
            return Date + time;
        }

        private static TimeSpan ConvertToTimeSpan(DateTime time)
        {
            return time.TimeOfDay;
        }
    }
}
