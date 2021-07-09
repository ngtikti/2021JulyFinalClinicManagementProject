using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using ClinicManagementProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace ClinicManagementProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IRepo<Doctor, string> _doctorrepo; 
        private readonly ILoginService<DoctorViewModel, string> _doctorlogin;
        private readonly IRepo<Patient, string> _patientrepo;
        private readonly IScheduleD<DoctorSchedule, List<int>> _doctorschedulerepo;
        private readonly IConsult<ConsultationDetail, int> _consultationDetailrepo;

        // private readonly IRepo<ConsultationDetail, List<int>> _consultationUpdaterepo;

        public DoctorController(IRepo<Doctor, string> doctorrepo, IRepo<Patient, string> patientrepo, IScheduleD<DoctorSchedule, List<int>> doctorschedulerepo, IConsult<ConsultationDetail, int> consultationDetailrepo, ILoginService<DoctorViewModel, string> doctorlogin)
        //public DoctorController(IRepo<Doctor, string> doctorrepo,  ILoginService<DoctorViewModel, string> doctorlogin)
        {
            //_context = context; //for passing context into actions in controller
            _patientrepo = patientrepo;
            _doctorlogin = doctorlogin;

            _doctorrepo = doctorrepo;

            _doctorschedulerepo = doctorschedulerepo;
            _consultationDetailrepo = consultationDetailrepo;

        }
        public ActionResult Login()
        {
            DoctorViewModel doctor = new DoctorViewModel();
            doctor.Username = Convert.ToString(TempData["DoctorUsername"]);
            return View(doctor); //get method to show login page
        }

        public ActionResult AboutUs()
        {
            ICollection<Doctor> Doctors = _doctorrepo.GetAll();
            return View(Doctors);
        }

        [HttpGet]
        public ActionResult ConsultationUpdate(int timeSlotId, int patId, int docId) //This is to get Consultation detail for Doctor to update
        {
            //ConsultationDetail consultationDetail = _consultationDetailrepo.Get(doc_id); //getting all the doctorschedule
            try
            {
                //ConsultationDetail consultationDetail = _consultationDetailrepo.Get(new List<int>() { timeSlotId, patId, docId }); //getting all the doctorschedule
                DoctorSchedule docsch = _doctorschedulerepo.GetAll().SingleOrDefault(ds => ds.Timeslot_Id == timeSlotId && ds.Doctor_Id == docId);
                string timeslot = docsch.Time;
                //ConsultationDetail consultationDetail = _consultationDetailrepo.GetAll().SingleOrDefault(cd => cd.Timeslot == timeslot && cd.Doctor_Id == docId && cd.Patient_Id == patId);
                IEnumerable<ConsultationDetail> cds = _consultationDetailrepo.GetAll().Where(cd => cd.Timeslot == timeslot && cd.Doctor_Id == docId && cd.Patient_Id == patId).ToList();
                ConsultationDetail consultationDetail = cds.LastOrDefault();
                
                if (consultationDetail == null)
                {
                    ViewData["Message"] = "Unable to get your Consultation Detail (TimeSlotID:" + timeSlotId + " , Patient ID: " + patId + ", DoctorID:" + docId + "), it might have already completed";
                    ViewData["docId"] = docId;
                    return View();
                }
                else
                {
                    //fill the default values if they are null
                    if (consultationDetail.Consultation_Date == null)
                        consultationDetail.Consultation_Date = System.DateTime.Now;

                    if (consultationDetail.Bill == null)
                        consultationDetail.Bill = 30;

                    //consultationDetail.Patient = _patientrepo.Get(patId);
                    //consultationDetail.Doctor = _doctorrepo.Get(docId);
                    consultationDetail.Patient = _patientrepo.GetAll().SingleOrDefault(p => p.Patient_Id == patId);
                    consultationDetail.Doctor = _doctorrepo.GetAll().SingleOrDefault(d => d.Doctor_Id == docId);

                    //get all consultation history of the patient and fill TempData to show to doctor.
                    ICollection<ConsultationDetail> consultationHistory = _consultationDetailrepo.GetAll(); //getting all the consultations
                    consultationHistory.Remove(consultationDetail);
                    consultationHistory.Where(c => c.Patient_Id == patId);

                    foreach (ConsultationDetail consItem in consultationHistory)
                    {
                        //if (consItem.Consultation_Date == null || consItem.Consultation_Date > System.DateTime.Now || consItem.Patient_Id != patId)
                        //    consultationHistory.Remove(consItem);
                        //else

                        //consItem.Doctor = _doctorrepo.Get(consItem.Doctor_Id);
                        consItem.Doctor = _doctorrepo.GetAll().SingleOrDefault(d => d.Doctor_Id == consItem.Doctor_Id);
                    }
                    ViewBag.ConsHistory = consultationHistory;
                    return View(consultationDetail);
                }
            }
            catch(Exception e)
            {
                ViewData["Message"] = "Unable to get your Consultation Detail, it might have already completed " + e.Message;
                return View();
            }            
        }

        [HttpPost]
        public ActionResult ConsultationUpdate(string consId, string consRemarks, string consStatus, string consBill, string consPatId, string consDocId, string consDate, string consTime)
        {
            bool schRemoved = false;
            bool consUpdateRes = false;
            int patId = 0;

            //This is to save updated Consultation detail done by the Doctor
            ConsultationDetail consDetail = new ConsultationDetail();
            consDetail.Consultation_Id = Convert.ToInt32(consId);
            consDetail.Doctor_Id = Convert.ToInt32(consDocId);
            consDetail.Patient_Id = Convert.ToInt32(consPatId);
            consDetail.Consultation_Date = Convert.ToDateTime(consDate);
            consDetail.Timeslot = consTime;
            consDetail.Consultation_Remarks = consRemarks;
            consDetail.Consultation_Status = consStatus;
            
            consDetail.Bill = Convert.ToInt32(consBill);

            //consUpdateRes = _consultationDetailrepo.Edit(new List<int>() {consDetail.Consultation_Id}, consDetail);
            consUpdateRes = _consultationDetailrepo.Edit(consDetail.Consultation_Id, consDetail);

            if (consUpdateRes)
            {
                //if successfully updated the Consultation and the Status is marked for Bill Payment, then Delete the Schedule
                if(consStatus.ToUpper() == "PENDING PAYMENT")
                {
                    //get all schedule for the doctor
                    //Compare it for the timeslot and Patient, then remove the Patient ID from the schedule
                    ICollection<DoctorSchedule> allDocSchedule = _doctorschedulerepo.GetAll(consDetail.Doctor_Id);
                    foreach (var schItem in allDocSchedule)
                    { 
                        //if Schedule Doc ID, Patient ID is matching, then Delete it Mona on 7th July
                        if(schItem.Doctor_Id == consDetail.Doctor_Id && schItem.Patient_Id == consDetail.Patient_Id && schItem.Time == consDetail.Timeslot)
                        {
                            //schRemoved = _doctorschedulerepo.Delete(new List<int>() { schItem.Timeslot_Id, consDetail.Doctor_Id });                            
                            schItem.Patient_Id = null;
                            schRemoved = _doctorschedulerepo.Edit(new List<int>() { schItem.Timeslot_Id, consDetail.Doctor_Id }, schItem);
                        }                       
                    }
                    if(schRemoved)
                        ViewData["Message"] = "Successfully updated Consultation and cleared Schedule (at " + System.DateTime.Now.ToString() + ")";
                    else
                        ViewData["Message"] = "Successfully updated Consultation but error in clearing chedule (at " + System.DateTime.Now.ToString() + ")";
                }
            }
            else
                ViewData["Message"] = "Error in saving Consultation Data. Please retry";

            //return view is erroring cshtml as the consultationDetail model is null,
            //so filling it up again and passing back to retail the information.
            //consDetail.Patient = _patientrepo.Get(consDetail.Patient_Id);
            //consDetail.Doctor = _doctorrepo.Get(consDetail.Doctor_Id);
            consDetail.Patient = _patientrepo.GetAll().SingleOrDefault(p => p.Patient_Id == consDetail.Patient_Id);
            consDetail.Doctor = _doctorrepo.GetAll().SingleOrDefault(d => d.Doctor_Id == consDetail.Doctor_Id);

            ICollection<ConsultationDetail> consultationHistory = _consultationDetailrepo.GetAll(); //getting all the consultations
            consultationHistory.Remove(consDetail);
            patId = Convert.ToInt32(consPatId);
            consultationHistory.Where(c => c.Patient_Id == patId);

            foreach (ConsultationDetail consItem in consultationHistory)
            {
                //if(consItem.Consultation_Date == null || consItem.Consultation_Date > System.DateTime.Now)
                //    consultationHistory.Remove(consItem);

                //consItem.Doctor = _doctorrepo.Get(consItem.Doctor_Id);
                consItem.Doctor = _doctorrepo.GetAll().SingleOrDefault(d => d.Doctor_Id == consItem.Doctor_Id);
            }
            ViewBag.ConsHistory = consultationHistory;

            return View(consDetail);
        }

        [HttpPost]            
        public ActionResult Login(DoctorViewModel doctor)
        {


            bool flag = _doctorlogin.Login(doctor);
            if (flag)
            {
                //log in success, means username and password correct
                ViewData["Message"] = "Welcome!";
                TempData["DoctorUsername"] = doctor.Username;
                return RedirectToAction("DoctorConsole", "Doctor", doctor); //view and controller syntax...and passing actual patient to the next...seems like patient to pass to next is too long due to password...have to only pass a part of it
            }
            else
            {
                //login fail
                ViewData["Message"] = "Invalid Username or Password";
                return View();//remain to same login page, and display invalid username or password
            }
        }
            public ActionResult Register()
            {
                DoctorViewModel doctorViewModel = new DoctorViewModel();
                return View(doctorViewModel); //get method to show login page
            }

        [HttpPost]
        public ActionResult Register(DoctorViewModel doctor)
        {
            bool flag = _doctorlogin.Register(doctor);
            if (flag)
            {
                TempData["DoctorUsername"] = doctor.Username;
                return RedirectToAction("Login");
            }
            ViewBag.Message = "Invalid entry. Please fill in again"; //if got time, think of a way to separate the diff errors (can use modelstate) (actually, invalidation is directly shown to cshtml if said. cool! now the only non mentioned, is invalid username)
            return View();
        }

        //Doctor console to link to all other modules
        public ActionResult DoctorConsole(Doctor d) //...since model is patient, passed has to be patient or child of patient, else null. to show patient console page...have action links to bookappointment (view doctor, view doctorschedule,update doctorschedule), access reportandbill, cancel existing appointment
        {
            Doctor doc = _doctorrepo.Get(TempData.Peek("DoctorUsername").ToString());//calling the actual patient using username in order to get the name to say welcome lol
            ViewData["Message"] = doc.Name;
            return View(doc);//...pat is to make sure model is refering to pat....else null error....should pass model.Username to the action links
        }
        public ActionResult ViewDoctor_Schedule(int consDocId)//this would be an action link to a post method (with doctorschedule model) to update doctorschedulerepo...getting the doctor_id as id, and patient_username as TempData["PatientUsername"]
        {
            try
            {
                //ICollection<DoctorSchedule> doctorSchedule = _doctorschedulerepo.GetAll(doc_id); //getting all the doctorschedule
                Doctor docplaceholder = _doctorrepo.GetAll().SingleOrDefault(d => d.Username == TempData.Peek("DoctorUsername").ToString());

                //ICollection<DoctorSchedule> doctorSchedule = _doctorschedulerepo.GetAll(consDocId); //getting all the doctorschedule
                ICollection<DoctorSchedule> doctorSchedule = _doctorschedulerepo.GetAll(docplaceholder.Doctor_Id);
                //fill all Patients, Mona added 6th July
                foreach (DoctorSchedule schedule in doctorSchedule)
                {
                    schedule.Patient = _patientrepo.GetAll().SingleOrDefault(p => p.Patient_Id == schedule.Patient_Id);
                }
                return View(doctorSchedule);
            }
            catch (Exception e)
            {
                ViewData["Message"] = "Error in getting Doctor Schedule " + e.Message;
                return View();
            }
        }

        
        public ActionResult ConsultationDetail(int patId, int docId)
        {
            //ConsultationDetail consultationDetail = _consultationDetailrepo.Get(doc_id); //getting all the doctorschedule
            //ConsultationDetail consultationDetail = _consultationDetailrepo.Get(new List<int>() { patId, docId }); //getting all the doctorschedule
            ConsultationDetail consultationDetail = _consultationDetailrepo.GetAll().SingleOrDefault(cd => cd.Patient_Id == patId && cd.Doctor_Id == docId); //might give error btw....one patient can have multiple timeslots for a doctor

            return View(consultationDetail);
        }
        
        public ActionResult ConsultationHistoryPatient(int id)
        {
            ICollection<ConsultationDetail> consultationDetail = _consultationDetailrepo.GetAll(); //getting all the consultations
            return View(consultationDetail);
        }
        public ActionResult ConsultationDetail(int id)
        {
            var docUsername = TempData["DoctorUsername"]; //retrieving patientusername from bookappointment action
            Doctor doc = _doctorrepo.Get(docUsername.ToString()); //getting the real patient
            int doc_id = doc.Doctor_Id; //getting the patient Id to pass onto next page in order to add into doctorschedule
            ICollection<ConsultationDetail> consultationDetail = _consultationDetailrepo.GetAll(doc_id); //getting all the doctorschedule

            return View(consultationDetail);
        }
    }
}

