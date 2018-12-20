using System.Collections.Generic;
using ChiroDashWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChiroDashWebApi.Services
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