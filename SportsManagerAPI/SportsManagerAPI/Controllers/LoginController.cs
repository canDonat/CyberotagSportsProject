using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SportsManagerAPI.Models;
using SportsManagerAPI.Repositories;
using SportsManagerAPI.Entities;

namespace JwtInDotnetCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IConfiguration _config;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public LoginController(IConfiguration config, IUserRepository userRepository, IMapper mapper)
		{
			_config = config;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginModel loginModel)
		{
			// LoginModel'den User'a dönüşüm
			var user = _mapper.Map<User>(loginModel);

			// Kullanıcı adı ve şifreyi doğrula
			var isValidUser = _userRepository.ValidateUser(user);

			if (!isValidUser)
			{
				return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
			}

			// JWT token oluştur
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Issuer"],
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: credentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

			return Ok(new { token });
		}
	}
}
