using System.Collections.Generic;
using System.Threading.Tasks;
using ChiroDash.Application.Doctors.Commands;
using ChiroDash.Application.Doctors.Models;
using ChiroDash.Application.Doctors.Queries;
using ChiroDash.Application.Scorecards.Queries;
using ChiroDash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IConfiguration config;

        public DoctorsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var getQuery = new GetDoctorsQuery(config);
            var doctors = await getQuery.Execute();

            var doctorsDto = AutoMapper.Mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Ok(doctorsDto);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}", Name = "GetDoctor")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var getQuery = new GetDoctorQuery(config);
            var doctor = await getQuery.Execute(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorDto = AutoMapper.Mapper.Map<DoctorDto>(doctor);

            return Ok(doctorDto);
        }


        // POST: api/Doctors
        [HttpPost(Name = "CreateDoctor")]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorToCreateDto doctorToCreate)
        {
            if (doctorToCreate == null)
            {
                return BadRequest();
            }

            var employee = AutoMapper.Mapper.Map<Employee>(doctorToCreate);

            var addCommand = new AddDoctorCommand(config);
            var (createdDoctor, target) = await addCommand.Execute(employee);

            return CreatedAtRoute("GetDoctor", new { id = createdDoctor.Id }, createdDoctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] DoctorToUpdateDto doctorToUpdateDto)
        {
            if (doctorToUpdateDto == null)
            {
                return BadRequest();
            }

            var getQuery = new GetDoctorQuery(config);
            var doctor = await getQuery.Execute(id);
            if (doctor == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(doctorToUpdateDto, doctor);

            var updateCommand = new UpdateDoctorCommand(config);
            await updateCommand.Execute(doctor);

            return Ok(doctor);
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var getQuery = new GetDoctorQuery(config);
            var doctor = await getQuery.Execute(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var deleteCommand = new DeleteDoctorCommand(config);
            await deleteCommand.Execute(doctor);

            return NoContent();
        }
    }
}
