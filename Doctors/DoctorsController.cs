using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ChiroDashWebApi.Doctors
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorServices service;

        public DoctorsController(IDoctorServices service)
        {
            this.service = service;
        }

        // GET: api/Doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> Get()
        {
            var doctors = service.GetDoctors();
            
            if (doctors.Count == 0)
            {
                return NotFound();
            }

            return doctors.Values;
        }

        // GET: api/Doctors/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Doctor> Get(int id)
        {
            var doctor = service.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // POST: api/Doctors
        [HttpPost]
        public ActionResult<Doctor> Post([FromBody] Doctor newDoctor)
        {
            var doctor = service.AddDoctor(newDoctor);


            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public ActionResult<Doctor> Put(int id, [FromBody] Doctor newDoctor)
        {
            var doctor = service.UpdateDoctorById(id, newDoctor);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public ActionResult<Doctor> Delete(int id)
        {
            var doctor = service.DeleteDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }
    }
}
