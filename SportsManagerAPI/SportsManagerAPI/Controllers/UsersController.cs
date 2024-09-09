using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;
using SportsManagerAPI.Repositories;

namespace SportsManagerAPI.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UsersController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		[HttpGet("{id:int}", Name = "[Controller]GetById")]
		public ActionResult<UserModel> GetById(int id)
		{
			var user = _userRepository.Get(id);
			if (user == null)
			{
				return NotFound();
			}
			var userModel = _mapper.Map<UserModel>(user);
			return Ok(userModel);
		}

		[HttpPost("", Name = "[Controller]InsertUser")]
		public ActionResult<int> InsertUser([FromBody] UserModel userModel)
		{
			if (userModel == null)
			{
				return BadRequest("User cannot be null");
			}

			var user = _mapper.Map<User>(userModel);
			var id = _userRepository.Insert(user);
			return CreatedAtAction(nameof(GetById), new { id = id }, id);
		}

		[HttpDelete("{id:int}", Name = "[Controller]DeleteUserById")]
		public IActionResult DeleteUserById(int id)
		{
			var deleted = _userRepository.Delete(id);
			if (!deleted)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
