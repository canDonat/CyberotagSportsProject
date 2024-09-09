namespace SportsManagerAPI.Models;

public class TeamModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ShortName { get; set; }
	public byte[]? Logo { get; set; }
}
