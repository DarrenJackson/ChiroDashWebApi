using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChiroDash.Application.Assistants.Models;
using ChiroDash.Application.Assistants.Queries;
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

            var doctorsDto = AutoMapper.Mapper.Map<IEnumerable<AssistantDto>>(doctors);

            return Ok(doctorsDto);
        }

    }
}
