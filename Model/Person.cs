using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}
