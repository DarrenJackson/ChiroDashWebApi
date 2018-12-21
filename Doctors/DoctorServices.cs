﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChiroDashWebApi.Doctors
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository repo;
        private readonly Dictionary<int, Doctor> doctors = new Dictionary<int, Doctor>();
        private int currentId = 0;

        public DoctorServices(IDoctorRepository repo)
        {
            this.repo = repo;
            var newId = currentId++;
            doctors[newId] = new Doctor()
            {
                Id = newId,
                Name = "Freddy"
            };

            newId = currentId++;
            doctors[newId] = new Doctor()
            {
                Id = newId,
                Name = "Bobby"
            };

            newId = currentId++;
            doctors[newId] = new Doctor()
            {
                Id = newId,
                Name = "Billy"
            };
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            var newId = currentId++;
            doctor.Id = newId;
            doctors[newId] = doctor;

            return doctors[newId];
        }

        public Dictionary<int, Doctor> GetDoctors()
        {
            return doctors;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            return await repo.GetById(id);
            //doctors.TryGetValue(id, out var doctor);
            //return doctor;
        }

        public Doctor UpdateDoctorById(int id, Doctor updatedDoctor)
        {
            updatedDoctor.Id = id;
            doctors[id] = updatedDoctor;
            return doctors[id];
        }

        public Doctor DeleteDoctorById(int id)
        {
            doctors.TryGetValue(id, out var doctor);
            doctors.Remove(id);
            return doctor;
        }
    }
}
