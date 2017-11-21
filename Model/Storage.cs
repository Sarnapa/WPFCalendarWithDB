using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    public class Storage
    {
        public List<Person> getPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }
    }
}
