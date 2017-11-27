﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

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
                Logger.Log.Info("Saved new appointment to database.");
            }
        }
        
        public void UpdateAppointment(Appointment appointment)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(appointment.AppointmentId);
                if (original != null)
                {
                    original.AppointmentDate = appointment.AppointmentDate;
                    original.StartTime = appointment.StartTime;
                    original.EndTime = appointment.EndTime;
                    original.Title = appointment.Title;
                    original.Description = appointment.Description;
                    db.Entry(original).OriginalValues["RowVersion"] = appointment.RowVersion;
                    try
                    {
                        db.SaveChanges();
                        Logger.Log.Info("Saved modified appointment to database.");
                    }
                    // ktos wczesniej nadpisal dane, a my ich nie pobralismy - nie akceptujemy naszej zmiany
                    catch (DbUpdateConcurrencyException)
                    {
                        Logger.Log.Error("Modifying appointment failure due to overwriting data by another user.");
                        throw new Exception("Modifying appointment failure due to overwriting data by another user.");
                    }
                }
                else
                {
                    Logger.Log.Error("Modifying appointment failure due to removing appointment by another user.");
                    throw new Exception("Modifying appointment failure due to removing appointment by another user.");
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
                    db.Entry(original).OriginalValues["RowVersion"] = appointment.RowVersion;
                    try
                    {
                        db.SaveChanges();
                        Logger.Log.Info("Remove appointment from database.");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Logger.Log.Error("Removing appointment failure due to overwriting data by another user.");
                        throw new Exception("Removing appointment failure due to overwriting data by another user.");
                    }
                }
                else
                {
                    Logger.Log.Error("Removing appointment failure due to removing appointment by another user.");
                    throw new Exception("Removing appointment failure due to removing appointment by another user.");
                }

            }
        }
    }
}
