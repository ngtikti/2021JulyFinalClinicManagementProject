using ClinicManagementProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClinicManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Controllers
{
    public class DoctorController : Controller
    {
        //private readonly ILogger<ClinicManagementContext> _logger;
        private readonly ILogger<DoctorController> _logger;
        private readonly IRepo<Doctor, string> _repo;

        public DoctorController(IRepo<Doctor, string> repo, ILogger<DoctorController> logger)
        {
            _logger = logger;
            _repo = repo;
        }

        
        public async Task<ActionResult> Index()
        {
            var doctors = await _repo.GetAll();
            return View(doctors);
        }

        
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                Doctor doctor = await _repo.Get(id);
                return View(doctor);
            }
            catch
            {
                _logger.LogError("Uable to get the edit");
            }
            return View();
        }

        // GET:DoctroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:DoctroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                      var id = await _repo.Add(doctor);
                    if (id.Doctor_Id!= -1)
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }


        public async Task<ActionResult> Edit(int id)
        {

            try
            {
                Doctor doctor = await _repo.Get(id);
                return View(doctor);
            }
            catch
            {
                _logger.LogError("Uable to get the edit");
            }
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Doctor doctor) //not getting doctor Id for some reason, nor id
        {
            try
            {
               
                Doctor myDoctor = await _repo.Update(id, doctor);
                //Doctor myDoctor = await _repo.Update(id, doctor);
                if (myDoctor != null)
                    return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit", doctor);
            }
            return View("Edit", doctor);
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
