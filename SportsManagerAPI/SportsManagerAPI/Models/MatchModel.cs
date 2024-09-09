namespace SportsManagerAPI.Models;

public class MatchModel
{
	public int Id { get; set; }
	public long HomeTeamId { get; set; }
	public long AwayTeamId { get; set; }
	public DateTime MatchDate { get; set; }
	public TimeSpan? MatchTime { get; set; }
	public int? HomeTeamScore { get; set; }
	public int? AwayTeamScore { get; set; }
	public string? Result { get; set; }
	public string Status { get; set; }
}
