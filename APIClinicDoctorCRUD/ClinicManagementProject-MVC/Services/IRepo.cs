using ClinicManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Services
{
    public interface IRepo<T, K>//T is type, K is parameter
    {

        public Task<T> Add(T t); //add a new T. eg. new doctor/patient/admin/doctorschedule

        Task<T> Get(K k); //get a T based on k as parameter

        Task<T> Get(int id); //get a T based on k as parameter

        public Task<ICollection<T>> GetAll();
        //public ICollection<T> GetAll(int id); //get a list of Ts

        public Task<T> Edit(K k, T t); //getting T given k as parameter
        public Task<T> Delete(K k);
        public Task<T> Update(int id, Doctor doctor);
    }
}
