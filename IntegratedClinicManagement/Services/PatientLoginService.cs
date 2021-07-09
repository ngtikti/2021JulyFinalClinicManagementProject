using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementProject.Services
{
    public class PatientLoginService : ILoginService<PatientViewModel,string>
    {
        private IRepo<Patient, string> _repo;

        public PatientLoginService(IRepo<Patient,string> patientRepo) //didnt add logger. ok?....dont need context, as its in patientrepo alr
        {
            _repo = patientRepo;
            
        }

        public bool Login(PatientViewModel t)
        {
            var pat = _repo.Get(t.Username);//getting pat in Patients table with username same as patientviewmodel t
            if (pat != null)
            {
                using var hmac = new HMACSHA512(pat.PasswordSalt); //using pat passwordsalt as salt for keyed in patient
                if (t.EnteredPassword == null) //if got username, but never key in pw
                {
                    return false;
                }
                var checkPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(t.EnteredPassword));//encrypting into byte[] for keyed in password in login field...
                //checking if the byte[] of pat is the same as the byte[] of patient
                for (int i = 0; i < checkPass.Length; i++)
                {
                    if (checkPass[i] != pat.Password[i])
                    {
                        return false; //password wrong

                        //ViewData["Message"] = "Wrong username or password";
                        //return View();
                    }
                }
                //ViewData["Message"] = "Welcome" + pat.Name;
                //TempData["PatientId"] = pat.Patient_Id;
                //TempData["PatientName"] = pat.Name;
                //return RedirectToAction("PatientConsole", "Patient"); //view and controller syntax
                return true; //return to patientconsole

            }
            else
                //    ViewData["Message"] = "Invalid Username or Password";
                //return View();//remain to same login page, and display invalid username or password
                return false; //wrong username, not found
        }

        public bool Register(PatientViewModel t)
        {
            if (t.EnteredPassword==t.RetypeEnteredPassword && t.Username!=null)//checking if patientviewmodel is entered correctly (includes inherited class of patient too)...with all the validations.eg. making sure required fields are keyed in, also checking through if password matches or not, else it will give exceptions
            {
                Patient myPatient = t;
                ICollection<Patient> patients = _repo.GetAll();
                //encrypting password
                using var hmac = new HMACSHA512();
                myPatient.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(t.EnteredPassword)); //encrypting keyed in password as password to myPatient.Password, with key
                myPatient.PasswordSalt = hmac.Key;

                //checking if username taken or not in patients
                bool usertaken = false;
                foreach (var item in patients)
                {
                    if (t.Username.ToLower() == item.Username.ToLower())//have to compare using lower as sql is case sensitive, will give fk error
                    {
                        usertaken = true;
                    }
                }
                if (usertaken == true)
                {
                    //ViewBag.Message = "Username taken, please use another";
                    //return View();
                    return false; //username taken
                }
                else
                {
                    _repo.Add(myPatient);  //with the password and passwordsalt added too
                    return true; //username ok, created
                }
            }
            //ViewBag.Message = "Please fill in all the fields accordingly";
            return false; //modelstate invalid, fill in properly
        }





    }
}
