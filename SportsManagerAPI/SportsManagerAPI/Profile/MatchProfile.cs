using AutoMapper;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;

public class MatchProfile : Profile
{
	public MatchProfile()
	{
		// Entity -> Model
		CreateMap<Match, MatchModel>();

		// Model -> Entity
		CreateMap<MatchModel, Match>();
	}
}
