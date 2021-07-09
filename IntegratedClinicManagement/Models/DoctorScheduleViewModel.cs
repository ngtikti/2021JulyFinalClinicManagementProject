using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementProject.Models
{
    public class DoctorScheduleViewModel : DoctorSchedule
    {
        public DoctorScheduleViewModel()
        {
        }
        [Required(ErrorMessage = "Schedule Time cannot be empty")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}$",
            ErrorMessage = "Enter a Valid Time with Format: XXXX - XXXX")]
        [Display(Name = "Time")]
        public string EnteredTime { get; set; } //since this is string, cannot be passed into database. so using a viewmodel to operate around that wont be passed into database

    }
}