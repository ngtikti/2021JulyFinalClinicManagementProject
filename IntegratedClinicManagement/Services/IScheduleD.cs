using System;
using System.Collections.Generic;

namespace ClinicManagementProject.Services
{
    public interface IScheduleD<T, K> : IRepo<T, K>
    {
        public bool AddSchedule(T t);

    }

}
