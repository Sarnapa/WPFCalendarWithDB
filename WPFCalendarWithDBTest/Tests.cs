using System;
using System.Data.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NUnit.Framework;
using WPFCalendarWithDB.ViewModel;
using WPFCalendarWithDB.Model;
using System.Linq;

namespace WPFCalendarWithDBTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void AddingNewAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment = new Appointment();
            appointment.AppointmentDate = DateTime.Now.Date;
            appointment.StartTime = new TimeSpan(11, 0, 0);
            appointment.EndTime = new TimeSpan(12, 0, 0);
            appointment.Title = "wydarzenie";
            appointment.Description = "opis";
            storage.CreateAppointment("user1", appointment);
            Appointment foundAppointment;
            using (var db = new StorageContext())
            {
                foundAppointment = db.Appointments.Find(appointment.AppointmentId);
            }
            Assert.IsNotNull(foundAppointment);
        }

        [Test]
        public async void ModifyingAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            appointment.Description = "new description";
            storage.UpdateAppointment(appointment);
            Appointment foundAppointment;
            using (var db = new StorageContext())
            {
                foundAppointment = db.Appointments.Find(appointment.AppointmentId);
            }
            Assert.AreEqual(appointment.Description, foundAppointment.Description);
        }

        [Test]
        public async void RemovingAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            storage.DeleteAppointment(appointment);
            Appointment foundAppointment;
            using (var db = new StorageContext())
            {
                foundAppointment = db.Appointments.Find(appointment.AppointmentId);
            }
            Assert.IsNull(foundAppointment);
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Modifying appointment failure due to overwriting data by another user.")]
        public async void ModifyingModifiedAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            appointment.Description = RandomString(10);
            storage.UpdateAppointment(appointment);
            appointment.Description = RandomString(8);
            storage.UpdateAppointment(appointment);  
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Modifying appointment failure due to removing appointment by another user.")]
        public async void ModifyingRemovedAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            storage.DeleteAppointment(appointment);
            appointment.Description = RandomString(8);
            storage.UpdateAppointment(appointment);
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Removing appointment failure due to overwriting data by another user.")]
        public async void RemovingModifiedAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            appointment.Description = RandomString(10);
            storage.UpdateAppointment(appointment);
            storage.DeleteAppointment(appointment);
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Removing appointment failure due to removing appointment by another user.")]
        public async void RemovingRemovedAppointmentTest()
        {
            var storage = new Storage();
            Appointment appointment;
            using (var db = new StorageContext())
            {
                appointment = await db.Appointments.FirstOrDefaultAsync();
            }
            storage.DeleteAppointment(appointment);
            storage.DeleteAppointment(appointment);
        }

        [Test]
        public void GoingNextWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetNextWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void GoingPrevWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetPrevWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(-7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void PopupMenuTest()
        {
            var mainVm = new MainViewModel();
            Assert.AreEqual(false, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(true, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(false, mainVm.IsPopup);
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
