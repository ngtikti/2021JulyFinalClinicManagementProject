using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicManagementProject.Models;

namespace ClinicManagementProject.Models
{
    public class ClinicManagementContext : DbContext
    {
        public ClinicManagementContext(DbContextOptions options) : base(options)//taking options from base
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        public DbSet<ConsultationDetail> ConsultationDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorSchedule>().HasKey(
                ds => new {ds.Doctor_Id, ds.Timeslot_Id}
                );

            modelBuilder.Entity<Admin>().HasData(
                new Admin()
                {
                    Admin_Id = 1,
                    Username="testadmin",
                    Password= new byte[1],
                    PasswordSalt = new byte[1],
                    Name = "MrTestAdmin"

                }
                ); 

            modelBuilder.Entity<Patient>().HasData(
                new Patient()
                {
                    Patient_Id=1,
                    Username = "abc",
                    Name = "Tim",
                    Age = 30,
                    Phone = "32423434",
                    Gender = "Male",
                    Password= new byte[1],
                    PasswordSalt=new byte[1]

                }
                );
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Doctor_Id=1,
                    Username="docabc",
                    Name="TimDoc",
                    Age=30,
                    Phone="323524523",
                    Gender="Male",
                    Password=new byte[1],
                    PasswordSalt=new byte[1], //just a place holder password to prevent exception..
                    Specialization="Childcare"
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
                    PasswordSalt = new byte[2], //just a place holder password to prevent exception..
                    Specialization="Oncology"
                });

            //preset doctor schedule (2 slots for each of the 2 preset doctors)
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule()
                {
                    Timeslot_Id=1,
                    Time="930-1030",
                    Doctor_Id=1
                });
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule()
                {
                    Timeslot_Id = 2,
                    Time = "1330-1430",
                    Doctor_Id = 1
                });
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule()
                {
                    Timeslot_Id = 1,
                    Time = "930-1030",
                    Doctor_Id = 2
                });
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule()
                {
                    Timeslot_Id = 2,
                    Time = "1330-1430",
                    Doctor_Id = 2
                });


        }

    }
}
