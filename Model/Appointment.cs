using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}
