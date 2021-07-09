using System;
using System.Collections.Generic;

namespace ClinicManagementProject.Services
{
    public interface IConsult<T, K> : IRepo<T, K>
    {
        public ICollection<T> GetAll(K k); //get a list of Ts
        public bool Update(string a);

    }

}
