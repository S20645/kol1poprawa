using kol1poprawa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol1poprawa.Services
{
    public interface IPrescriptionService
    {
        public List<Prescription> GetPrescriptions();

        public Boolean IsDoctorIDExists(int id);

        public int AddDoctor(Doctor doctor);
    }
}
