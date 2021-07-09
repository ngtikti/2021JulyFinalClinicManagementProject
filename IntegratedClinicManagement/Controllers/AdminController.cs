using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagementProject.Models;
using ClinicManagementProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepo<Admin, string> _adminRepo;
        private readonly ILoginService<AdminViewModel, string> _adminLogin;
        private readonly IRepo<Doctor, string> _doctorRepo;
        private readonly ILoginService<DoctorViewModel, string> _doctorLogin;
        //private readonly IRepo<DoctorSchedule, List<int>> _doctorScheduleRepo;
        private readonly IRepo<Patient, string> _patientRepo;
        private readonly ILoginService<PatientViewModel, string> _patientLogin;
        private readonly IConsult<ConsultationDetail, int> _consultRepo;
        private readonly IScheduleD<DoctorSchedule, List<int>> _scheduleRepo;

        public AdminController(IScheduleD<DoctorSchedule, List<int>> scheduleRepo, IConsult<ConsultationDetail, int> consultRepo, IRepo<Patient, string> patientRepo, ILoginService<PatientViewModel, string> patientLogin, IRepo<Doctor, string> doctorRepo, ILoginService<DoctorViewModel, string> doctorLogin, IRepo<Admin, string> adminRepo, ILoginService<AdminViewModel, string> adminLogin)
        {
            //_context = context; //for passing context into actions in controller
            _adminRepo = adminRepo;
            _adminLogin = adminLogin;
            _doctorRepo = doctorRepo;
            _doctorLogin = doctorLogin;
            //_doctorScheduleRepo = doctorScheduleRepo;
            _patientRepo = patientRepo;
            _patientLogin = patientLogin;
            _consultRepo = consultRepo;
            _scheduleRepo = scheduleRepo;


        }


        public ActionResult Login()
        {
            AdminViewModel admin = new AdminViewModel();
            admin.Username = Convert.ToString(TempData.Peek("AdminUsername"));

            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminViewModel admin)
        {
           
                bool flag = _adminLogin.Login(admin);
                if (flag)
                {
                    //log in success, means username and password correct
                    ViewData["Message"] = "Welcome!";
                    TempData["AdminUsername"] = admin.Username;
                    return RedirectToAction("AdminConsole", "Admin"); //view and controller syntax...and passing actual admin to the next...seems like ? to pass to next is too long due to password...have to only pass a part of it
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
            AdminViewModel admin = new AdminViewModel();
            return View(admin);
        }

        [HttpPost]
        public ActionResult Register(AdminViewModel admin)
        {
            if (ModelState.IsValid)
            {
                bool flag = _adminLogin.Register(admin);
                if (flag)
                {
                    TempData["AdminUsername"] = admin.Username;
                    return RedirectToAction("Login", "Admin");
                }
                ViewBag.Message = "Invalid entry. Please fill in again"; //if got time, think of a way to separate the diff errors (can use modelstate) (actually, invalidation is directly shown to cshtml if said. cool! now the only non mentioned, is invalid username)
                return View();
            }
            AdminViewModel admine = new AdminViewModel();
            return View(admine);
        }


        public ActionResult AdminConsole()
        {
            Admin ad = _adminRepo.Get(Convert.ToString(TempData.Peek("AdminUsername")));
            ViewData["Message"] = "Welcome " + ad.Name;
            return View(ad);
        }

        public ActionResult DocRegNew()
        {
            DoctorViewModel doctor = new DoctorViewModel();

            return View(doctor);
        }

        [HttpPost]
        public ActionResult DocRegNew(DoctorViewModel doc)
        {
            if (ModelState.IsValid)
            {
                bool flag = _doctorLogin.Register(doc);
                if (flag)
                {
                    ViewData["Successful"] = "The registration of " + doc.Name + " is successfully registed.";

                    return RedirectToAction("AdminConsole");
                }


                return View();
            }
            DoctorViewModel doctor = new DoctorViewModel();

            return View(doctor);
        }

        public ActionResult DocMain()
        {
            ViewData["Message"] = "Welcome " + TempData.Peek("AdminUsername");

            ICollection<Doctor> doctors = _doctorRepo.GetAll();
            return View(doctors);
        }

        public ActionResult DocViewSchedule(int docid)//this would be an action link to a post method (with doctorschedule model) to update doctorschedulerepo...getting the doctor_id as id, and patient_username as TempData["PatientUsername"]
        {
            TempData["DocId"] = docid;

            ICollection<DoctorSchedule> doctorSchedule = _scheduleRepo.GetAll(docid); //getting all the doctorschedule according to doctorId
            if (doctorSchedule.Count() == 0)
            {
                @ViewBag.Message = "No Schedule Found, Add a Schedule? Click ";
            }
            return View(doctorSchedule); //displaying only the slots where patient_id has not been input
        }

        public ActionResult DocAddTimeSlot(int docid)
        {
            DoctorScheduleViewModel timeSlot = new DoctorScheduleViewModel();
            //timeSlot.Doctor_Id = Convert.ToInt32(TempData.Peek("DocId")); // new line
            TempData["DocId"] = docid;
            timeSlot.Doctor_Id = docid;
            return View(timeSlot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// giving issue
        public ActionResult DocAddTimeSlot(DoctorScheduleViewModel timeSlot)
        {
            timeSlot.Doctor_Id= Convert.ToInt32(TempData.Peek("DocId"));
            if (ModelState.IsValid)// giving issue

            {
                DoctorSchedule newTimeSlot = timeSlot;
                newTimeSlot.Time = timeSlot.EnteredTime;
                newTimeSlot.Doctor_Id = Convert.ToInt32(TempData.Peek("DocId"));
                bool flag = _scheduleRepo.AddSchedule(newTimeSlot);
                if (flag)
                {
                    return RedirectToAction("DocViewSchedule", "Admin", new { docid = newTimeSlot.Doctor_Id });
                }
                ViewBag.Message = "Invalid entry. Please fill in again"; //if got time, think of a way to separate the diff errors (can use modelstate) (actually, invalidation is directly shown to cshtml if said. cool! now the only non mentioned, is invalid username)
                return View();
            }
            DoctorScheduleViewModel timeSlote = new DoctorScheduleViewModel();
            timeSlote.Doctor_Id = Convert.ToInt32(TempData.Peek("DocId")); // new line
            return View(timeSlote);
        }

        public ActionResult PatMain()
        {
            ViewData["Message"] = "Welcome " + TempData.Peek("AdminUsername");

            ICollection<Patient> patients = _patientRepo.GetAll();
            return View(patients);
        }

        public ActionResult PatRegNew()
        {
            PatientViewModel patient = new PatientViewModel();

            return View(patient);
        }

        [HttpPost]
        public ActionResult PatRegNew(PatientViewModel pat)
        {
            if (ModelState.IsValid)
            {
                bool flag = _patientLogin.Register(pat);
                if (flag)
                {
                    ViewData["Successful"] = "The registration of " + pat.Name + " is successfully registed.";

                    return RedirectToAction("AdminConsole");
                }


                return View();
            }
            PatientViewModel patient = new PatientViewModel();

            return View(patient);
        }

        public ActionResult PatPayment()
        {
            string pend = "Pending Payment";
            //ICollection<ConsultationDetail> cDetail = _consultRepo.GetAll(pend);
            ICollection<ConsultationDetail> cDetail = _consultRepo.GetAll();
            if (cDetail == null)
            {
                cDetail = new List<ConsultationDetail>(); //have to have something to pass to view
            }
            else
            {
                cDetail = _consultRepo.GetAll().Where(p => p.Consultation_Status.ToLower() == pend.ToLower()).ToList();
            }
            return View(cDetail);
        }

        public ActionResult PatPaidPass(int conid)
        {
            string id = Convert.ToString(conid);
            _consultRepo.Update(id);

            return RedirectToAction("PatPayment");
        }
        public ActionResult PatPaidAllPass(int conid, int patiid)
        {

            string id = Convert.ToString(conid);
            _consultRepo.Update(id);

            return RedirectToAction("PatAllCon","Admin", new { patid = patiid });
        }

        public ActionResult PatSearchAll()
        {

            Patient patient = new Patient();

            return View(patient);
        }

        [HttpPost]
        public ActionResult PatSearchAll(Patient patient)
        {
            
            return RedirectToAction("PatSearch", new { pat = patient.Name });
        }

        public ActionResult PatSearch(string pat)
        {
            ICollection<Patient> patients = _patientRepo.GetAll().Where(n => n.Name.ToLower() == pat.ToLower()).ToList();
            if(patients.Count() == 0)
            {
                @ViewBag.Message = "No Record Found, ";
            }
            return View(patients);
        }

        public ActionResult PatViewAll()
        {
            ICollection<Patient> patients = _patientRepo.GetAll().ToList();
            return View(patients);
        }
        public ActionResult PatAllCon(int patid)
        {
            //ICollection<ConsultationDetail> patAllCons = _consultRepo.GetAll(patid).ToList(); // will give error as int in getall is pertaining to docid, not patid
            ICollection<ConsultationDetail> patAllCons = _consultRepo.GetAll();
            if(patAllCons == null)
            {
                patAllCons = new List<ConsultationDetail>(); //because view cant take null
            }
            else
            {
                patAllCons = _consultRepo.GetAll().Where(cd => cd.Patient_Id == patid).ToList();
            }
            //if (_consultRepo.GetAll().Where(cd => cd.Patient_Id == patid) == null)
            //{
            //    patAllCons = new List<ConsultationDetail>();
            //}
            //else
            //{
            //    patAllCons = _consultRepo.GetAll().Where(cd => cd.Patient_Id == patid).ToList();
            //}
            return View(patAllCons);
        }



    }
}