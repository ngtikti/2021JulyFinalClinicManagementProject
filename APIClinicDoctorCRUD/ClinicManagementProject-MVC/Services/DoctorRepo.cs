using ClinicManagementProject.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ClinicManagementProject.Services
{
    public class DoctorRepo : IRepo<Doctor, string>
    {
        //private readonly ClinicManagementContext _context;
        //private readonly ILogger<DoctorRepo> _logger;
        //public DoctorRepo(ClinicManagementContext context, ILogger<DoctorRepo> logger)
        //{
        //    _context = context;
        //    _logger = logger;
        //}
        
        private readonly ILogger<DoctorRepo> _logger;
        public DoctorRepo( ILogger<DoctorRepo> logger)
        {
            
            _logger = logger;
        }

        //public async Task<int> Add(Doctor t)
        //{
        //    try
        //    {
        //        Doctor doctor = new Doctor();
        //        using (var httpClient = new HttpClient())
        //        {
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
        //            using (var response = await httpClient.PostAsync("http://localhost:27393/api/Doctor", content))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    string apiResponse = await response.Content.ReadAsStringAsync();
        //                    doctor = JsonConvert.DeserializeObject<Doctor>(apiResponse);
        //                    return doctor.Doctor_Id;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("Unable to add Doctor through API " + e.Message);
        //    }
        //    return -1;
        //}

        public Task<Doctor> Delete(string k)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> Edit(string k, Doctor t)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> Get(string k)
        {
            throw new NotImplementedException();
        }

        public async Task<Doctor> Get(int id)
        {
            try
            {
                Doctor doctor = new Doctor();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:27393/api/Doctor/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            doctor = JsonConvert.DeserializeObject<Doctor>(apiResponse);
                            return doctor;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to retrive Doctor from API " + e.Message);
            }
            return null;
        }

        public async Task<ICollection<Doctor>> GetAll()
        {
            try
            {
                List<Doctor> doctors = new List<Doctor>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:27393/api/Doctor/"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            doctors = JsonConvert.DeserializeObject<List<Doctor>>(apiResponse);
                            return doctors;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to retrive Doctor from API " + e.Message);
            }
            return null;
        }

        public async Task<Doctor> Update(int id, Doctor d)
        {
            try
            {
                Doctor doctor = new Doctor();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(d), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://localhost:27393/api/Doctor/" + id, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            doctor = JsonConvert.DeserializeObject<Doctor>(apiResponse);
                            return doctor;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to retrive pizza from API " + e.Message);
            }
            return null;
        }

        public async Task<Doctor> Add(Doctor t)
        {
            try
            {
                Doctor doctor = new Doctor();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:27393/api/Doctor", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            doctor = JsonConvert.DeserializeObject<Doctor>(apiResponse);
                            return doctor;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to add Doctor through API " + e.Message);
            }
            return null;
        }
    }
}
