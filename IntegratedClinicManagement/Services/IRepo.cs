using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Services
{
    public interface IRepo<T,K>//T is type, K is parameter
    {

        public bool Add(T t); //add a new T. eg. new doctor/patient/admin/doctorschedule

        public T Get(K k); //get a T based on k as parameter

        public ICollection<T> GetAll();
        public ICollection<T> GetAll(int id); //get a list of Ts
        
        public bool Edit(K k, T t); //getting T given k as parameter
        public bool Delete(K k);
    }
}
