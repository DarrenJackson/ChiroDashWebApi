using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChiroDashWebApi.Doctors
{
    public interface IDoctorServices
    {
        Doctor AddDoctor(Doctor doctor);

        Dictionary<int, Doctor> GetDoctors();

        Task<Doctor> GetDoctorById(int id);

        Doctor UpdateDoctorById(int id, Doctor updatedDoctor);

        Doctor DeleteDoctorById(int id);
    }
}