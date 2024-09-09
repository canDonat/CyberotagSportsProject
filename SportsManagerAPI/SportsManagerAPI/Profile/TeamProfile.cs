using AutoMapper;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;

public class TeamProfile : Profile
{
	public TeamProfile()
	{
		// Entity -> Model
		CreateMap<Team, TeamModel>();

		// Model -> Entity
		CreateMap<TeamModel, Team>();
	}
}
