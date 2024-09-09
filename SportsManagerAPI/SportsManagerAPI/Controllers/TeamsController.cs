using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;
using SportsManagerAPI.Repositories;

namespace SportsManagerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamsController : ControllerBase
{
	private readonly ITeamRepository _teamRepository;
	private readonly IMapper _mapper;

	public TeamsController(ITeamRepository teamRepository, IMapper mapper)
	{
		_teamRepository = teamRepository;
		_mapper = mapper;
	}

	[HttpGet("", Name = "[Controller]GetAll")]
	public IEnumerable<TeamModel> GetTakimlar()
	{
		var teams = _teamRepository.GetAll();
		return _mapper.Map<IEnumerable<TeamModel>>(teams);
	}

	[HttpGet("{id:int}", Name = "[Controller]GetById")]
	public ActionResult<TeamModel> GetById(int id)
	{
		var team = _teamRepository.Get(id);
		if (team == null)
		{
			return NotFound();
		}
		var teamModel = _mapper.Map<TeamModel>(team);
		return Ok(teamModel);
	}

	[HttpPost("", Name = "[Controller]Insertteam")]
	public ActionResult<int> Insertteam([FromBody] TeamModel teamModel)
	{
		var team = _mapper.Map<Team>(teamModel);
		var id = _teamRepository.Insert(team);
		return CreatedAtAction(nameof(GetById), new { id = id }, id);
	}

	[HttpPut("{id:int}", Name = "[Controller]Updateteam")]
	public IActionResult UpdateTakim(int id, [FromBody] TeamModel teamModel)
	{
		if (teamModel.Id != id)
		{
			return BadRequest();
		}

		var team = _mapper.Map<Team>(teamModel);
		var updated = _teamRepository.Update(team);
		if (!updated)
		{
			return NotFound();
		}
		return NoContent();
	}

	[HttpDelete("{id:int}", Name = "[Controller]DeleteTakimById")]
	public IActionResult DeleteteamById(int id)
	{
		var deleted = _teamRepository.Delete(id);
		if (!deleted)
		{
			return NotFound();
		}
		return NoContent();
	}
}
