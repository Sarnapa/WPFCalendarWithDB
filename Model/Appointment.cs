using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }
        [MaxLength(16), Required]
        public string Title { get; set; }
        [MaxLength(50), Required]
        public string Description { get; set; }
        [Column(TypeName = "date"), Required]
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "time"), Required]
        public TimeSpan StartTime { get; set; }
        [Column(TypeName = "time"), Required]
        public TimeSpan EndTime { get; set; }
        public virtual List<Attendance> Attendances { get; set; }

        public Appointment()
        {
            AppointmentId = Guid.NewGuid();
        }
    }
}
