using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using Microsoft.Extensions.Logging;

namespace ClinicManagementProject.Services
{
    public class DoctorRepo : IRepo<Doctor, string>//string is to pass Username from doctor class
    {
        private readonly ClinicManagementContext _context;
        private readonly ILogger<DoctorRepo> _logger;

        public DoctorRepo(ClinicManagementContext context, ILogger<DoctorRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Add(Doctor t)
        {
            try
            {
                _context.Doctors.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("Doctor registered", t);
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Could not register Doctor " + DateTime.Now.ToString());
                _logger.LogError("The details " + e.Message);
            }
            return false;
        }

        public bool Delete(string k)
        {
            throw new NotImplementedException();
        }

        public bool Edit(string k, Doctor t)
        {
            throw new NotImplementedException();
        }

        public Doctor Get(string k)
        {
            var doctor = _context.Doctors.SingleOrDefault(p => p.Username == k);
            return doctor;
        }
        public Doctor Get(int docId)
        {
            var doctor = _context.Doctors.SingleOrDefault(p => p.Doctor_Id == docId);
            return doctor;
        }
        public ICollection<Doctor> GetAll()
        {
            if (_context.Doctors.Count() == 0)
            {
                _logger.LogInformation("No doctor record");
                return null;
            }
            return _context.Doctors.ToList();
        }

        public ICollection<Doctor> GetAll(int id)
        {
            throw new NotImplementedException();
        }
    }
}
