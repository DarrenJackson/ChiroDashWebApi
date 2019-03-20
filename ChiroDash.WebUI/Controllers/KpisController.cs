using System.Collections.Generic;
using System.Threading.Tasks;
using ChiroDash.Application.Kpis.Commands;
using ChiroDash.Application.Kpis.Models;
using ChiroDash.Application.Kpis.Queries;
using ChiroDash.Domain.Entities;
using ChiroDash.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.WebUI.Controllers
{
    [Route("api/doctors/{doctorId}/kpis")]
    [ApiController]
    public class KpisController : ControllerBase
    {
        private readonly IConfiguration config;

        public KpisController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/doctors/1/Kpis
        [HttpGet(Name = "GetKpisByMonthForDoctor")]
        public async Task<ActionResult<IEnumerable<KpiDto>>> GetKpisForDoctor(string doctorId, [FromQuery] KpiResourceParameters resourceParams)
        {
            var getQuery = new GetKpisQuery(config);
            var cards = await getQuery.Execute(doctorId, resourceParams);

            if (cards == null)
            {
                return NotFound();
            }


            var cardsDto = AutoMapper.Mapper.Map<IEnumerable<KpiDto>>(cards);

            return Ok(cardsDto);
        }

        // GET: api/doctors/5/Kpis/2
        [HttpGet("{id}", Name = "GetKpiForDoctor")]
        public async Task<ActionResult<KpiDto>> GetKpiForDoctor(string doctorId, string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var getQuery = new GetKpiQuery(config);
            var card = await getQuery.Execute(doctorId, id);

            if (card == null)
            {
                return NotFound();
            }

            var kpiDto = AutoMapper.Mapper.Map<KpiDto>(card);

            return Ok(kpiDto);
        }

        // POST: api/Doctors/1/Kpis
        [HttpPost]
        public async Task<ActionResult<KpiDto>> CreateKpi([FromBody] KpiToCreateDto kpiToCreate, string doctorId)
        {
            if (kpiToCreate == null)
            {
                return BadRequest();
            }

            var card = AutoMapper.Mapper.Map<Kpi>(kpiToCreate);
            card.DoctorId = doctorId;

            var addCommand = new AddKpiCommand(config);
            var kpiCreates = await addCommand.Execute(card);

            return CreatedAtRoute("GetKpiForDoctor", new { doctorId, id = kpiCreates.Id }, kpiCreates);

        }

        // PUT: api/doctors/1/Kpis/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] KpiToUpdateDto kpiToUpdate, string doctorId, string id)
        {
            if (kpiToUpdate == null)
            {
                return BadRequest();
            }

            var getQuery = new GetKpiQuery(config);
            var card = await getQuery.Execute(doctorId, id);
            if (card == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(kpiToUpdate, card);

            var updateCommand = new UpdateKpiCommand(config);
            await updateCommand.Execute(card);

            return Ok(card);
        }

        // DELETE: api/doctors/1/Kpis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string doctorId, string id)
        {
            var getQuery = new GetKpiQuery(config);
            var card = await getQuery.Execute(doctorId, id);
            if (card == null)
            {
                return NotFound();
            }

            var deleteCommand = new DeleteKpiCommand(config);
            await deleteCommand.Execute(card);

            return NoContent();
        }
    }
}
