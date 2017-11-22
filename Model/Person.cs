using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Person
    {
        public Guid PersonId { get; set; }
        [MaxLength(16)]
        public string FirstName { get; set; }
        [MaxLength(16)]
        public string LastName { get; set; }
        public string UserID { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}
