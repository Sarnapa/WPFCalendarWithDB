﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public static class CalendarService
    {
        public static int GetWeekOfYear(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
        }

        public static DateTime GetFirstDateOfWeek(DateTime date)
        {
            // niedziela
            if ((int)date.DayOfWeek == 0)
                return date.AddDays(-6);
            return date.AddDays(-(int)date.DayOfWeek + 1);
        }
    }
}
