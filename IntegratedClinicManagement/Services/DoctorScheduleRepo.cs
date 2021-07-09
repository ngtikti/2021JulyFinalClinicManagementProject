using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicManagementProject.Services
{
    public class DoctorScheduleRepo : IScheduleD<DoctorSchedule, List<int>> //List<int> is to pass composite key {Timeslot_Id, Doctor_Id}
    {
        private readonly ClinicManagementContext _context;
        private readonly ILogger<DoctorScheduleRepo> _logger;

        public DoctorScheduleRepo(ClinicManagementContext context, ILogger<DoctorScheduleRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Add(DoctorSchedule t)
        {
            try
            {
                _context.DoctorSchedules.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("Doctor registered", t);
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Could not register doctor " + DateTime.Now.ToString());
                _logger.LogError("The details " + e.Message);
            }
            return false;
        }


        public bool Delete(List<int> k) //deleting the entire schedule slot
        {
            try
            {
                var doctorSchedule = Get(k);
                _context.DoctorSchedules.Remove(doctorSchedule);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError("Unable to delete the timeslot" + k + " " + e.Message);
            }
            return false;
        }

        public bool Edit(List<int> k, DoctorSchedule t)
        {
            try
            {
                _context.Update(t);//this requires get to be called first
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update doctor schedule " + k + e.Message);
                return false;
            }
            
        }

        public DoctorSchedule Get(List<int> k)
        {
            try
            {
                var doctorSchedule = _context.DoctorSchedules.SingleOrDefault(ds => ds.Timeslot_Id == k[0] && ds.Doctor_Id == k[1]);
                return doctorSchedule;
            }
            catch(Exception e)
            {
                _logger.LogError("No slot with this id " + k + " " + e.Message);
            }
            return null;
            
        }

        public ICollection<DoctorSchedule> GetAll()
        {
            if (_context.DoctorSchedules.Count() == 0)
            {
                _logger.LogInformation("No schedule found");
                return null;
            }
            return _context.DoctorSchedules.ToList();
        }

        public ICollection<DoctorSchedule> GetAll(int id) //additional method to get schedule by doctor id 
        {
            if (_context.DoctorSchedules.Where(ds=>ds.Doctor_Id==id).Count() == 0)
            {
                _logger.LogInformation("No schedule found");
                return _context.DoctorSchedules.Include(c => c.Patient).Where(ds => ds.Doctor_Id == id).ToList();
            }
            return _context.DoctorSchedules.Include(c => c.Patient).Where(ds => ds.Doctor_Id == id).ToList();
        }

        public bool AddSchedule(DoctorSchedule t)
        {
            DoctorSchedule sch = t;
            ICollection<DoctorSchedule> schedules = GetAll(sch.Doctor_Id);
            bool timetaken = false;
            int count = 0;
            if(schedules.Count() != 0)
            {
                foreach (var item in schedules)
                {
                    if (t.Time == item.Time)
                    {
                        timetaken = true;
                        break;
                    }
                    if (item.Timeslot_Id > 0)
                        count = item.Timeslot_Id;
                }
            }
            if (timetaken == true)
                return false;
            else
            {
                sch.Timeslot_Id += count + 1;
                Add(sch);
                return true;
            }
        }
    }
}
