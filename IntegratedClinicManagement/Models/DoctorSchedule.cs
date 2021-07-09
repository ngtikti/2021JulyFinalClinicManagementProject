using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Models
{
    public class DoctorSchedule
    {
        public int Timeslot_Id { get; set; }//timeslot ID...pre-seeded, slot 1, slot 2 ,slot 3...composite key with Doctor_Id

        public int Doctor_Id { get; set; }//composite key with Timeslot_Id...only one doctor_id and one slot_id....for patient to choose...and patient_id can be deleted to free up doctor's time....display and add if null
        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }

        [RegularExpression(@"[0-9]{4}\s-\s[0 - 9]{4}", ErrorMessage = "key Time in the following Format: XXXX - XXXX")]
        public string Time { get; set; }//timing of slot...pre-seeded...slot 1 to timing 1, timing 2, timing 3
        
        //the patient to be added to the slot. can be nullable
        public int? Patient_Id { get; set; }
        [ForeignKey("Patient_Id")]
        public Patient Patient { get; set; }
    }
}
