using AutoMapper;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;

public class LoginProfile : Profile
{
	public LoginProfile()
	{
		// LoginModel'den User'a dönüşüm
		CreateMap<LoginModel, User>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

		// User'dan LoginModel'e dönüşüm
		CreateMap<User, LoginModel>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
	}
}
