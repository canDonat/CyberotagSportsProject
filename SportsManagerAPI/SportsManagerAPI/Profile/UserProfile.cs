using AutoMapper;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;

public class UserProfile : Profile
{
	public UserProfile()
	{
		// Entity -> Model
		CreateMap<User, UserModel>();

		// Model -> Entity
		CreateMap<UserModel, User>();
	}
}
