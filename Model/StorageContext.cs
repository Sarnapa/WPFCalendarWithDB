using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    // kontekst bazy danych - obiekty odpowiadające fizycznej bazy danych
    class StorageContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().Property(a => a.RowVersion).IsRowVersion();
            base.OnModelCreating(modelBuilder);
        }*/ 
    }
}
