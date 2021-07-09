using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Models
{
    public class Doctor
    {
        [Key] 
        public int Doctor_Id { get; set; }

        //username and password
        //[Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }//validation done in patient register controller, so wont allowed to be repeated

        public byte[] Password { get; set; } //since passing to database, cannot be seen, must be encrypted
        public byte[] PasswordSalt { get; set; }//salt for encryption, passed to database as well

        //Doctor info
        //[Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Gender cannot be empty")]
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
                
    }
}
