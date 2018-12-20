using System.Collections.Generic;

namespace ChiroDashWebApi.Doctors
{
    public interface IDoctorServices
    {
        Doctor AddDoctor(Doctor doctor);

        Dictionary<int, Doctor> GetDoctors();

        Doctor GetDoctorById(int id);

        Doctor UpdateDoctorById(int id, Doctor updatedDoctor);

        Doctor DeleteDoctorById(int id);
    }
}