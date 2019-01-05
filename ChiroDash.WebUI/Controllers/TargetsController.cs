using System.Collections.Generic;
using System.Threading.Tasks;
using ChiroDash.Application.Targets.Commands;
using ChiroDash.Application.Targets.Models;
using ChiroDash.Application.Targets.Queries;
using ChiroDash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.WebUI.Controllers
{
    [Route("api/doctors/{doctorId}/targets")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        private readonly IConfiguration config;

        public TargetsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/Doctors/5/Targets
        [HttpGet(Name = "GetTargetsForDoctor")]
        public async Task<ActionResult<IEnumerable<TargetDto>>> GetTargetsForDoctor(int doctorId)
        {
            var getQuery = new GetDoctorTargetsQuery(config);
            var targets = await getQuery.Execute(doctorId);

            if (targets == null)
            {
                return NotFound();
            }

            var targetsDto = AutoMapper.Mapper.Map<IEnumerable<TargetDto>>(targets);

            return Ok(targetsDto);
        }

        // GET: api/Doctors/1/Targets/5
        [HttpGet("{id}", Name = "GetTargetForDoctor")]
        public async Task<ActionResult<TargetDto>> GetTargetForDoctor(string doctorId, string id)
        {
            var getQuery = new GetTargetQuery(config);
            var target = await getQuery.Execute(doctorId, id);

            if (target == null)
            {
                return NotFound();
            }

            var targetDto = AutoMapper.Mapper.Map<TargetDto>(target);

            return Ok(targetDto);
        }


        [HttpPost]
        public async Task<ActionResult> CreateTarget(string doctorId, [FromBody] TargetToCreateDto targetToCreate)
        {
            if (targetToCreate == null)
            {
                return BadRequest();
            }

            var target = AutoMapper.Mapper.Map<Target>(targetToCreate);
            target.DoctorId = doctorId;

            var addCommand = new AddTargetCommand(config);
            var createdTarget = await addCommand.Execute(target);

            //return Created($@"/api/Targets/{createdTarget.Id}", createdTarget);
            return CreatedAtRoute("GetTargetForDoctor", new { doctorId, id = createdTarget.Id }, createdTarget);

        }

        // PUT: api/Doctors/1/Targets/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put( string doctorId, string id, [FromBody] TargetToUpdateDto targetToUpdate)
        {
            if (targetToUpdate == null)
            {
                return BadRequest();
            }

            var getQuery = new GetTargetQuery(config);
            var target = await getQuery.Execute(doctorId, id);
            if (target == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(targetToUpdate, target);

            var updateCommand = new UpdateTargetCommand(config);
            await updateCommand.Execute(target);

            return Ok(target);
        }

        // DELETE: api/Doctors/1/Targets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string doctorId, string id)
        {
            var getQuery = new GetTargetQuery(config);
            var target = await getQuery.Execute(doctorId, id);
            if (target == null)
            {
                return NotFound();
            }

            var deleteCommand = new DeleteTargetCommand(config);
            await deleteCommand.Execute(target);

            return NoContent();
        }
    }
}
