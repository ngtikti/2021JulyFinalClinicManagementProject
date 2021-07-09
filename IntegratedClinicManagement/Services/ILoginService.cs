using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagementProject.Services
{
    public interface ILoginService<T, K>//T should be patientviewmodel 

    {
        //doctorviewmodel, patientviewmodel, adminviewmodel

        public bool Login(T t);//getting true if viewmodel input successfully  login

        public bool Register(T t);//returns true if successfully registered viewmodel input


    }
}
