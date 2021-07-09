using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using Microsoft.Extensions.Logging;

namespace ClinicManagementProject.Services
{
    //actually, patient crud should be here. rename as patientrepo

    //need methods to bookappointment, access reportandbill, cancel existing appointment (seed some doctors and schedule first), for other repo
    public class PatientRepo : IRepo<Patient, string>//get patient by username as its not repeated also
    {
        private readonly ClinicManagementContext _context;
        private readonly ILogger<PatientRepo> _logger;

        public PatientRepo(ClinicManagementContext context, ILogger<PatientRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Add(Patient t) //method to add patient...for registering possibly
        {
            try
            {
                _context.Patients.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("Patient registered", t);
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Could not register patient " + DateTime.Now.ToString());
                _logger.LogError("The details " + e.Message);
            }
            return false;
        }

        public Patient Get(string k) //method to get patient by username...for log in possibly
        {
            try
            {
                var patient = _context.Patients.SingleOrDefault(p => p.Username == k);
                return patient;
            }
            catch (Exception e)
            {
                var patient = new Patient(); //null patient
                return patient;
            }
 
        }

        public ICollection<Patient> GetAll() // method to retrieve patient list
        {
            if (_context.Patients.Count() == 0)
            {
                _logger.LogInformation("No patient record");
                return null;
            }
            return _context.Patients.ToList();

        }
        public bool Delete(string k) 
        {
            throw new NotImplementedException();
        }

        public bool Edit(string k, Patient t)
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

        public ICollection<Patient> GetAll(int id)
        {
            throw new NotImplementedException();
        }
    }
}
