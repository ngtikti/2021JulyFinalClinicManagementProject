using System;
using System.Collections.Generic;
using System.Linq;
using ClinicManagementProject.Models;
using Microsoft.Extensions.Logging;

namespace ClinicManagementProject.Services
{
    public class AdminRepo : IRepo<Admin, string>//get admin by username as its not repeated also
    {
        private readonly ClinicManagementContext _context;
        private readonly ILogger<AdminRepo> _logger;

        public AdminRepo(ClinicManagementContext context, ILogger<AdminRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Add(Admin t) //method to add admin...for registering possibly
        {
            try
            {
                _context.Admins.Add(t);
                _context.SaveChanges();
                _logger.LogInformation("Admin registered", t);
                return true;
            }

            catch (Exception e)
            {
                _logger.LogError("Could not register admin " + DateTime.Now.ToString());
                _logger.LogError("The details " + e.Message);
            }
            return false;
        }

        public Admin Get(string k) //method to get admin by username...for log in possibly
        {
            var admin = _context.Admins.SingleOrDefault(p => p.Username == k);
            return admin;
        }

        public ICollection<Admin> GetAll() // method to retrieve admin list
        {
            if (_context.Admins.Count() == 0)
            {
                _logger.LogInformation("No admin record");
                return null;
            }
            return _context.Admins.ToList();

        }
        public bool Delete(string k)
        {
            throw new NotImplementedException();
        }

        public bool Edit(string k, Admin t)
        {
            throw new NotImplementedException();
        }

        public ICollection<Admin> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddSchedule(Admin t)
        {
            throw new NotImplementedException();
        }
    }
}
