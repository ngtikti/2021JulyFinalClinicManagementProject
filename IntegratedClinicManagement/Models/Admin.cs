using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }

        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }//validation done in admin register controller, so wont allowed to be repeated

        public byte[] Password { get; set; } //since passing to database, cannot be seen, must be encrypted
        public byte[] PasswordSalt { get; set; }//salt for encryption, passed to database as well



        //Admin info
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
    }
}
