using Microsoft.Extensions.Logging;
using ClinicManagementWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementWebService.Services
{
    public class DoctorRepo : IRepo<Doctor, int>
    {
        private readonly ClinicManagementContext _context;
        private readonly ILogger<DoctorRepo> _logger;

        public DoctorRepo(ClinicManagementContext context, ILogger<DoctorRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public int Add(Doctor t)
        {
            try
            {
                _context.Doctors.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("Doctor registered", t);
                return t.Doctor_Id;
            }

            catch (Exception e)
            {
                _logger.LogError("Could not register Doctor " + DateTime.Now.ToString());
                _logger.LogError("The details " + e.Message);
            }
            return -1;
        }

        public Doctor Delete(int k)
        {
            try
            {
                var doctor = Get(k);
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                return doctor;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to delete the doctor details" + k + " " + e.Message);
            }
            return null;
        }

        public Doctor Get(int k)
        {
            try
            {
                var doctor = _context.Doctors.Single(p => p.Doctor_Id == k);
                return doctor;
            }
            catch (Exception e)
            {
                _logger.LogError("No Doctor with this id " + k + " " + e.Message);
            }
            return null;
        }

        public ICollection<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctor Update(int k, Doctor t) //swagger works good. but t.doctor_Id has to be specified in post
        {
            try
            {
                //Doctor doc = Get(k);
                //doc = t;
                _context.Update(t);
                _context.SaveChanges();
                return t;
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update the Doctor details" + k + " " + e.Message);
            }
            return null;
        }
    }
}
