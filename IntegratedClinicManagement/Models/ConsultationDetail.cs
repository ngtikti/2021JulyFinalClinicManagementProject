using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Models
{
    public class ConsultationDetail
    {
        [Key]
        public int Consultation_Id { get; set; }

        //foreign keys
        public int Patient_Id { get; set; } //need this as patientId in doctorschedule will be wiped as doctorschedule is freed up
        [ForeignKey("Patient_Id")]
        public Patient Patient {get;set;}

        public int Doctor_Id { get; set; }
        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }

        //properties
        public string Timeslot{ get; set; } //time of appointment...maybe can replace with timeslot_id....entered moment of appointment booking to link consultationdetail with doctorschedule....impt in case patient wants to cancel appointment

        public DateTime? Consultation_Date { get; set; } //date of consultation....updated by doctor module using DateTime.Now after consultation is done, to keep for records
        public string? Consultation_Remarks { get; set; } //consultation history....can be added by doctor...report is here too
        public string Consultation_Status { get; set; } //once consultation remarks done, consultation is done, either "opened", "pending payment", "closed" //when booking, can mark to be open by patient module... pending payment by doctor/admin after consultation, and closed after patient made payment through payment module
        public int? Bill { get; set; } //can be accessed and added by doctor/ admin ...can be "null"

    }
}
