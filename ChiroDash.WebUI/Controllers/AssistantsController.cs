using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Application.Assistants.Commands;
using ChiroDash.Application.Assistants.Models;
using ChiroDash.Application.Assistants.Queries;
using ChiroDash.Application.Doctors.Commands;
using ChiroDash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.WebUI.Controllers
{
    [Route("api/Assistants")]
    [ApiController]
    public class AssistantsController : ControllerBase
    {
        private readonly IConfiguration config;

        public AssistantsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/Assistants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssistantDto>>> GetAllAssistants()
        {
            var getQuery = new GetAssistantsQuery(config);
            var doctors = await getQuery.Execute();

            var assistantDto = AutoMapper.Mapper.Map<IEnumerable<AssistantDto>>(doctors);

            return Ok(assistantDto);
        }

        // GET: api/Assistants
        [HttpGet("{id}", Name = "GetAssistant")]
        public async Task<ActionResult<AssistantDto>> GetAssistant(int id)
        {
            var getQuery = new GetAssistantQuery(config);
            var assistant = await getQuery.Execute(id);

            var assistantDto = AutoMapper.Mapper.Map<AssistantDto>(assistant);

            return Ok(assistantDto);
        }

        // POST: api/Assistants
        [HttpPost(Name = "CreateAssistant")]
        public async Task<ActionResult> CreateDoctor([FromBody] AssistantToCreateDto assistantToCreate)
        {
            if (assistantToCreate == null)
            {
                return BadRequest();
            }

            assistantToCreate.Id = 0;
            var doctor = AutoMapper.Mapper.Map<Assistant>(assistantToCreate);

            var addCommand = new AddAssistantCommand(config);
            var createdAssistant = await addCommand.Execute(doctor);

            return CreatedAtRoute("GetAssistant", new { id = createdAssistant.Id }, createdAssistant);
        }

        // DELETE: api/Assistants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var getQuery = new GetAssistantQuery(config);
            var assistant = await getQuery.Execute(id);
            if (assistant == null)
            {
                return NotFound();
            }

            var deleteCommand = new DeleteAssistantCommand(config);
            await deleteCommand.Execute(assistant);

            return NoContent();
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AssistantToUpdateDto assistantToUpdateDto)
        {
            if (assistantToUpdateDto == null)
            {
                return BadRequest();
            }

            if (id != assistantToUpdateDto.Id)
            {
                return BadRequest();
            }


            var getQuery = new GetAssistantQuery(config);
            var assistant = await getQuery.Execute(id);
            if (assistant == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(assistantToUpdateDto, assistant);

            var updateCommand = new UpdateAssistantCommand(config);
            await updateCommand.Execute(assistant);

            return Ok(assistant);
        }

    }
}
