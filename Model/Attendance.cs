using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Attendance
    {
        public virtual Appointment Appointment { get; set; }
        public virtual Person Person { get; set; }
        public bool accepted { get; set; }
    }
}
