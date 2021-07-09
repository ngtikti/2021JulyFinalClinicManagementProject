using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ClinicManagementProject.Models;

namespace ClinicManagementProject.Services
{
    public class AdminLoginService : ILoginService<AdminViewModel, string>
    {
        private IRepo<Admin, string> _repo;

        public AdminLoginService(IRepo<Admin, string> adminRepo) //didnt add logger. ok?....dont need context, as its in adminrepo alr
        {
            _repo = adminRepo;

        }

        public bool Login(AdminViewModel t)
        {
            var pat = _repo.Get(t.Username);//getting pat in admins table with username same as adminviewmodel t
            if (pat != null)
            {
                using var hmac = new HMACSHA512(pat.PasswordSalt); //using pat passwordsalt as salt for keyed in admin
                if (t.EnteredPassword == null) //if got username, but never key in pw
                {
                    return false;
                }
                var checkPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(t.EnteredPassword));//encrypting into byte[] for keyed in password in login field...
                //checking if the byte[] of pat is the same as the byte[] of admin
                for (int i = 0; i < checkPass.Length; i++)
                {
                    if (checkPass[i] != pat.Password[i])
                    {
                        return false; //password wrong

                     
                    }
                }
                
                return true; //return to adminconsole

            }
            else
                //    ViewData["Message"] = "Invalid Username or Password";
                //return View();//remain to same login page, and display invalid username or password
                return false; //wrong username, not found
        }

        public bool Register(AdminViewModel t)
        {
            if (t.EnteredPassword == t.RetypeEnteredPassword && t.Username != null)//checking if patientviewmodel is entered correctly (includes inherited class of patient too)...with all the validations.eg. making sure required fields are keyed in, also checking through if password matches or not, else it will give exceptions
            {
                Admin myAdmin = t;
                ICollection<Admin> admins = _repo.GetAll();
                //encrypting password
                using var hmac = new HMACSHA512();
                myAdmin.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(t.EnteredPassword)); //encrypting keyed in password as password to myPatient.Password, with key
                myAdmin.PasswordSalt = hmac.Key;
                  
                //checking if username taken or not in patients
                bool usertaken = false;
                foreach (var item in admins)
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
                    _repo.Add(myAdmin);  //with the password and passwordsalt added too
                    return true; //username ok, created
                }
            }
            //ViewBag.Message = "Please fill in all the fields accordingly";
            return false; //modelstate invalid, fill in properly
        }





    }
}
