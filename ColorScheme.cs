using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB
{
    public class ColorScheme
    {
        public String SchemeName
        {
            get; set;
        }
        public String ButtonBackgroundColor
        {
            get; set;
        }
        public String IsMouseOverButtonBackgroundColor
        {
            get; set;
        }
        public String IsPressedButtonBackgroundColor
        {
            get; set;
        }
        public String WeekDayLabelBackgroundColor
        {
            get; set;
        }
        public String WeekLabelBackgroundColor
        {
            get; set;
        }
        public String DayCellBackgroundColor
        {
            get; set;
        }
        public String TodayCellBackgroundColor
        {
            get; set;
        }
        public String MainFontColor
        {
            get; set;
        }
        public String AppointmentColor
        {
            get; set;
        }

        public ColorScheme(String schemeName, String buttonBackgroundColor, String isMouseOverButtonBackgroundColor,
            String isPressedButtonBackgroundColor, String weekdayLabelBackgroundColor, String weekLabelBackgroundColor, 
            String dayCellBackgroundColor, String todayCellBackgroundColor, String mainFontColor, String appointmentColor)
        {
            SchemeName = schemeName;
            ButtonBackgroundColor = buttonBackgroundColor;
            IsMouseOverButtonBackgroundColor = isMouseOverButtonBackgroundColor;
            IsPressedButtonBackgroundColor = isPressedButtonBackgroundColor;
            WeekDayLabelBackgroundColor = weekdayLabelBackgroundColor;
            WeekLabelBackgroundColor = weekLabelBackgroundColor;
            DayCellBackgroundColor = dayCellBackgroundColor;
            TodayCellBackgroundColor = todayCellBackgroundColor;
            MainFontColor = mainFontColor;
            AppointmentColor = appointmentColor;
        }
    }
}
