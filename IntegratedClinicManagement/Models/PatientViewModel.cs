using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Models
{
    public class PatientViewModel : Patient
    {
        [Required(ErrorMessage = "Password cannot be empty")]
        [Display(Name="Password")]
        public string EnteredPassword { get; set; } //since this is string, cannot be passed into database. so using a viewmodel to operate around that wont be passed into database

        [Required(ErrorMessage ="Please re-enter your password again")]
        [Display(Name="Re-type Password")]
        [Compare("EnteredPassword", ErrorMessage ="Password mismatch")]//validation directly to check if equal, if not equal, will remain at page with controller using ModelState.IsValid
        public string RetypeEnteredPassword { get; set; }

    }
}
