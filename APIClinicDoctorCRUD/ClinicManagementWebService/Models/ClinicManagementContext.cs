using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementWebService.Models
{
    public class ClinicManagementContext : DbContext
    {
        public ClinicManagementContext(DbContextOptions options) : base(options)//taking options from base
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Doctor_Id = 1,
                    Username = "docabc",
                    Name = "TimDoc",
                    Age = 30,
                    Phone = "323524523",
                    Gender = "Male",
                    Password = new byte[1],
                    PasswordSalt = new byte[1] //just a place holder password to prevent exception..
                });
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Doctor_Id = 2,
                    Username = "docoof",
                    Name = "TiDoc",
                    Age = 30,
                    Phone = "323524523",
                    Gender = "Male",
                    Password = new byte[2],
                    PasswordSalt = new byte[2] //just a place holder password to prevent exception..
                }); 
        }
        //public DbSet<ClinicManagementProject.Models.DoctorSchedule> DoctorSchedule { get; set; }
    }
}
