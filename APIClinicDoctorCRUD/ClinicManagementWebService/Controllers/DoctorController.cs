using ClinicManagementWebService.Models;
using ClinicManagementWebService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicManagementWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepo<Doctor, int> _repo;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IRepo<Doctor, int> repo, ILogger<DoctorController> logger)
        //public DoctorController(IRepo<Doctor, string> doctorrepo,  ILoginService<DoctorViewModel, string> doctorlogin)
        {
            _repo = repo;
            _logger = logger;
        }

            // GET: api/<DoctorController>
        [HttpGet]

        public IActionResult Get() //swagger working good as long as dont specify identity
        {
            try
            {
                var doctor = _repo.GetAll();
                return Ok(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not get all Doctors " + e.Message);
            }
            return BadRequest("No Doctor Registered ");
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) //swagger working good as long as dont specify identity
        {
            try
            {
                var doctor = _repo.Get(id);
                if (doctor != null)
                    return Ok(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not get Doctor list " + e.Message);
            }
            return BadRequest("Unable to fetch the Doctor List");
        }

        // POST api/<DoctorController>
        [HttpPost]
        public IActionResult Post([FromBody] Doctor doctor) //swagger working good as long as dont specify identity
        {
            try
            {
                int id = _repo.Add(doctor); 
                if (id != -1)
                    return Ok(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not register Doctor " + e.Message);
            }
            return BadRequest("Doctor not added");
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Doctor doctor) //swagger works good. but t.doctor_Id has to be specified in post
        {
            try
            {
                var myDoctor = _repo.Update(id, doctor);
                if (myDoctor != null)
                    return Ok(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not update Doctor " + e.Message);
            }
            return BadRequest("Doctor details not updated");
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) //swagger works good.
        {
            try
            {
                var myDoctor = _repo.Delete(id);
                if (myDoctor != null)
                    return Ok(myDoctor);
            }
            catch (Exception e)
            {
                _logger.LogError("Could not delete " + e.Message);
            }
            return BadRequest("Doctor not delete");
        }
    }
}
