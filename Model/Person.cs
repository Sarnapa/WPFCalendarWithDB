using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        [MaxLength(16), Required]
        public string FirstName { get; set; }
        [MaxLength(16), Required]
        public string LastName { get; set; }
        [MaxLength(10), Required]
        public string UserID { get; set; }
        public virtual List<Attendance> Attendances { get; set; }

        public Person()
        {
            PersonId = Guid.NewGuid();
        }
    }
}
