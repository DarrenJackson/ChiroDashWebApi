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

        // GET: api/doctors
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

        // GET: api/Doctor/5
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

        // POST: api/Doc
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

        // PUT: api/Doc/5
        [HttpPut("{id}")]
        public ActionResult<Doctor> Put(int id, [FromBody] Doctor updatedDoctor)
        {
            var doctor = service.UpdateDoctorById(id, updatedDoctor);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        // DELETE: api/ApiWithActions/5
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
