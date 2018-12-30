using System.Collections.Generic;
using System.Threading.Tasks;
using ChiroDash.Application.Scorecards;
using ChiroDash.Application.Scorecards.Commands;
using ChiroDash.Application.Scorecards.Models;
using ChiroDash.Application.Scorecards.Queries;
using ChiroDash.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChiroDash.WebUI.Controllers
{
    [Route("api/doctors/{doctorId}/scorecards")]
    [ApiController]
    public class ScorecardsController : ControllerBase
    {
        private readonly IConfiguration config;

        public ScorecardsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET: api/doctors/1/Scorecards
        [HttpGet(Name = "GetScorecardsForDoctor")]
        public async Task<ActionResult<IEnumerable<ScorecardDto>>> Get(string doctorId)
        {
            var getQuery = new GetScorecardsQuery(config);
            var cards = await getQuery.Execute(doctorId);

            if (cards == null)
            {
                return NotFound();
            }


            var cardsDto = AutoMapper.Mapper.Map<IEnumerable<ScorecardDto>>(cards);

            return Ok(cardsDto);
        }


        // GET: api/doctors/5/Scorecards/2
        [HttpGet("{id}", Name = "GetScorecardForDoctor")]
        public async Task<ActionResult<ScorecardDto>> GetScorecardsForDoctor(string doctorId, string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var getQuery = new GetScorecardQuery(config);
            var card = await getQuery.Execute(doctorId, id);

            if (card == null)
            {
                return NotFound();
            }

            var scorecardDto = AutoMapper.Mapper.Map<ScorecardDto>(card);

            return Ok(scorecardDto);
        }

        // POST: api/Doctors/1/Scorecards
        [HttpPost]
        public async Task<ActionResult<ScorecardDto>> CreateScorecard([FromBody] ScorecardToCreateDto scorecardToCreate, string doctorId)
        {
            if (scorecardToCreate == null)
            {
                return BadRequest();
            }

            var card = AutoMapper.Mapper.Map<Scorecard>(scorecardToCreate);
            card.DoctorId = doctorId;

            var addCommand = new AddScorecardCommand(config);
            var createdCard = await addCommand.Execute(card);

            return CreatedAtRoute("GetScorecardForDoctor", new { doctorId, id = createdCard.Id }, createdCard);

        }

        // PUT: api/doctors/1/Scorecards/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] ScorecardToUpdateDto scorecardToUpdate, string doctorId, string id)
        {
            if (scorecardToUpdate == null)
            {
                return BadRequest();
            }

            var getQuery = new GetScorecardQuery(config);
            var card = await getQuery.Execute(doctorId, id);
            if (card == null)
            {
                return NotFound();
            }

            AutoMapper.Mapper.Map(scorecardToUpdate, card);

            var updateCommand = new UpdateScorecardCommand(config);
            await updateCommand.Execute(card);

            return Ok(card);
        }

        // DELETE: api/doctors/1/Scorecards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string doctorId, string id)
        {
            var getQuery = new GetScorecardQuery(config);
            var card = await getQuery.Execute(doctorId, id);
            if (card == null)
            {
                return NotFound();
            }

            var deleteCommand = new DeleteScorecardCommand(config);
            await deleteCommand.Execute(card);

            return NoContent();
        }
    }
}
