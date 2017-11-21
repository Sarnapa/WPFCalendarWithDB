using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WPFCalendarWithDB.Model;

namespace WPFCalendarWithDB.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly int _weekDaysCount = 7;
        private static readonly int _weeksCount = 4;
        private List<Day> _daysList = new List<Day>();
        private List<String> _weeksList = new List<string>();
        //private List<EventModel> _allEventsList = new List<EventModel>();
        private Boolean _isPopup;
        private List<ColorScheme> _colorSchemesList;
        private ColorScheme _currentColorScheme;
        private List<String> _fontsList;
        private String _currentFont;
        private ICommand _getPrevWeek;
        private ICommand _getNextWeek;
        private ICommand _popupCommand;

        public int WeekDaysCount 
        {
            get
            {
                return _weekDaysCount;
            }
        }

        public int WeeksCount 
        {
            get
            {
                return _weeksCount;
            }
        }
        
        public List<Day> DaysList 
        {
            get
            {
                return _daysList;
            }
            set
            {
                _daysList = value;
                OnPropertyChanged("DaysList");
            }
        }

        public List<String> WeeksList
        {
            get
            {
                return _weeksList;
            }
            set
            {
                _weeksList = value;
                OnPropertyChanged("WeeksList");
            }
        }

        public Boolean IsPopup
        {
            get
            {
                return _isPopup;
            }
            private set
            {
                _isPopup = value;
                OnPropertyChanged("IsPopup");
            }
        }

        public List<ColorScheme> ColorSchemesList
        {
            get
            {
                return _colorSchemesList;
            }
        }

        public ColorScheme CurrentColorScheme
        {
            get
            {
                return _currentColorScheme;
            }
            set
            {
                _currentColorScheme = value;
                if (DaysList.Count > 0)
                {
                    DateTime firstDateOfWeek = CalendarService.GetFirstDateOfWeek(DaysList[0].Date.Date);
                    DaysList = GetDaysList(firstDateOfWeek);
                }
                OnPropertyChanged("CurrentColorScheme");
            }
        }

        public List<String> FontsList
        {
            get
            {
                return _fontsList;
            }
        }

        public String CurrentFont
        {
            get
            {
                return _currentFont;
            }
            set
            {
                _currentFont = value;
                OnPropertyChanged("CurrentFont");
            }
        }

        public ICommand GetPrevWeek
        {
            get
            {
                return _getPrevWeek;
            }
        }

        public ICommand GetNextWeek
        {
            get
            {
                return _getNextWeek;
            }
        }

        public ICommand PopupCommand
        {
            get
            {
                return _popupCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public MainViewModel()
        {
            _colorSchemesList = new List<ColorScheme>
            {
                new ColorScheme("Basic Scheme", "#CCFFFF", "#009999", "#006666", "#AAAAFF", "#AAAAFF", "#FFFFFF", "#FFFF42", "#000066", "#007777"),
                new ColorScheme("Dark Scheme", "#F9A825", "#FF8F00", "#EF6C00", "#424242", "#424242", "#999999", "#8BC34A", "#FAFAFA", "#FFEA00"),
                new ColorScheme("Green Scheme", "#B2FF59", "#76FF03", "#64DD17", "#4CAF50", "#4CAF50", "#C5E1A5", "#F9A825", "#212121", "#37474F")
            };
            CurrentColorScheme = _colorSchemesList[0];

            _fontsList = new List<String>
            {
                "Arial",
                "Courier New",
                "Times New Roman"
            };
            CurrentFont = _fontsList[0];

            /*_allEventsList = (List<EventModel>)SerializationService.ReadSource();*/
            DateTime firstDateOfWeek = CalendarService.GetFirstDateOfWeek(DateTime.Now.Date);
            DaysList = GetDaysList(firstDateOfWeek);
            WeeksList = GetWeeksList(firstDateOfWeek);

            _getPrevWeek = new RelayCommand(GetPrevWeekAction, null);
            _getNextWeek = new RelayCommand(GetNextWeekAction, null);
            _popupCommand = new RelayCommand(e => IsPopup = !IsPopup, null);
        }

        private void OnPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<Day> GetDaysList(DateTime date)
        {
            List<Day> daysList = new List<Day>();
            int calendarCellsCount = _weekDaysCount * _weeksCount;
            for (int i = 0; i < calendarCellsCount; ++i)
            {
                if (date.CompareTo(DateTime.Now.Date) == 0)
                    daysList.Add(new Day(date, CurrentColorScheme.TodayCellBackgroundColor, CurrentColorScheme.AppointmentColor,
                        CurrentColorScheme.MainFontColor));
                else
                    daysList.Add(new Day(date, CurrentColorScheme.DayCellBackgroundColor, CurrentColorScheme.AppointmentColor,
                        CurrentColorScheme.MainFontColor));
                date = date.AddDays(1);
            }
            return daysList;
        }

        private List<String> GetWeeksList(DateTime date)
        {
            List<String> weeksList = new List<String>();

            int weekNumber = CalendarService.GetWeekOfYear(date);
            for(int i = 0; i < _weeksCount; ++i)
            {
                weeksList.Add(String.Format("W{0}\n{1}", weekNumber, date.Year));
                date = date.AddDays(7);
                weekNumber = CalendarService.GetWeekOfYear(date);
            }
            return weeksList;
        }

        public void AddAppointment(Day day, AppointmentModel e)
        {
            day.AddAppointment(e);
        }

        public void ModifyAppointment(Day day)
        {
            day.AppointmentsList = new ObservableCollection<AppointmentModel>(day.AppointmentsList.OrderBy(o => o.Start));
        }

        public void RemoveAppointment(Day day, AppointmentModel e)
        {
            day.RemoveAppointment(e);
        }

        // Commands Actions
        private void GetPrevWeekAction(object obj)
        {
            DateTime firstDate = DaysList[0].Date.AddDays(-7);
            DaysList = GetDaysList(firstDate);
            WeeksList = GetWeeksList(firstDate);
        }

        private void GetNextWeekAction(object obj)
        {
            DateTime firstDate = DaysList[0].Date.AddDays(7);
            DaysList = GetDaysList(firstDate);
            WeeksList = GetWeeksList(firstDate);
        }

    }
}
