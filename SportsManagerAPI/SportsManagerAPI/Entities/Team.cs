namespace SportsManagerAPI.Entities;

public class Team
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string ShortName { get; set; }
	public byte[]? Logo { get; set; }
}
