using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Attendance
    {
        [Key, Column(Order = 0), ForeignKey("Appointment")]
        public Guid AppointmentId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Person")]
        public Guid PersonId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Person Person { get; set; }
        public bool accepted { get; set; }
    }
}
