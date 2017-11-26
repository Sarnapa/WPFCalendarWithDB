using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCalendarWithDB.Model
{
    class Storage
    {
        public List<Appointment> GetDayAppointments(String userID, DateTime day)
        {
            using (var db = new StorageContext())
            {
                return db.Appointments.Where(e => e.AppointmentDate == day && e.Attendances.Where(a => a.Person.UserID == userID).ToList().Count != 0).ToList();
            }
        }

        public Guid GetPersonID(String userID)
        {
            using (var db = new StorageContext())
            {
                return db.Persons.Where(p => p.UserID == userID).ToList().FirstOrDefault().PersonId;
            }
        }

        public void CreateAppointment(String userID, Appointment appointment)
        {
            using (var db = new StorageContext())
            {   
                db.Appointments.Add(appointment);
                Guid personID = GetPersonID(userID);
                Attendance attendance = new Attendance();
                attendance.PersonId = personID;
                attendance.AppointmentId = appointment.AppointmentId;
                db.Attendances.Add(attendance);
                db.SaveChanges();
            }
        }

        public void UpdateAppointment(Appointment newAppointment)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(newAppointment.AppointmentId);
                if (original != null)
                {
                    original.AppointmentDate = newAppointment.AppointmentDate;
                    original.StartTime = newAppointment.StartTime;
                    original.EndTime = newAppointment.EndTime;
                    original.Title = newAppointment.Title;
                    original.Description = newAppointment.Description;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteAppointment(Appointment appointment)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(appointment.AppointmentId);
                if (original != null)
                {
                    db.Appointments.Remove(original);
                    db.SaveChanges();
                }
            }
        }
    }
}
