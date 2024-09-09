using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;
using SportsManagerAPI.Repositories;

namespace ProjectManagementForDevelopersWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MatchesController : ControllerBase
	{
		private readonly IMatchRepository _matchRepository;
		private readonly IMapper _mapper;

		public MatchesController(IMatchRepository matchRepository, IMapper mapper)
		{
			_matchRepository = matchRepository;
			_mapper = mapper;
		}

		[HttpGet("", Name = "[Controller]GetAll")]
		public ActionResult<IEnumerable<MatchModel>> GetAll()
		{
			var matches = _matchRepository.GetAll();
			var matchModel = _mapper.Map<IEnumerable<MatchModel>>(matches);  // Entity -> Model dönüşümü
			return Ok(matchModel);
		}

		[HttpGet("{id:int}", Name = "[Controller]GetById")]
		public ActionResult<MatchModel> GetById(int id)
		{
			var match = _matchRepository.Get(id);
			if (match == null)
			{
				return NotFound();
			}

			var matchModel = _mapper.Map<MatchModel>(match);  // Entity -> Model dönüşümü
			return Ok(matchModel);
		}

		[HttpPost("", Name = "[Controller]InsertMatch")]
		public ActionResult<int> InsertMatch([FromBody] MatchModel matchModel)
		{
			if (matchModel == null)
			{
				return BadRequest("Match cannot be null");
			}

			var match = _mapper.Map<Match>(matchModel);  // Model -> Entity dönüşümü

			var id = _matchRepository.Insert(match);
			return CreatedAtAction(nameof(GetById), new { id = id }, id);
		}

		[HttpPut("{id:int}", Name = "[Controller]UpdateMatch")]
		public IActionResult UpdateMatch(int id, [FromBody] MatchModel matchModel)
		{
			if (matchModel == null || matchModel.Id != id)
			{
				return BadRequest();
			}

			var match = _mapper.Map<Match>(matchModel);  // Model -> Entity dönüşümü

			var updated = _matchRepository.Update(match);
			if (!updated)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("{id:int}", Name = "[Controller]DeleteMatchById")]
		public IActionResult DeleteMatchById(int id)
		{
			var deleted = _matchRepository.Delete(id);
			if (!deleted)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
