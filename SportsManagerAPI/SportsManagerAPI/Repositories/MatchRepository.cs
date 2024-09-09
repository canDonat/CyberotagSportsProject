using Dapper;
using SportsManagerAPI.Database;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Repositories.Abstractions;

namespace SportsManagerAPI.Repositories;

public interface IMatchRepository : IRepository<Match>
{
}

public class MatchRepository : IMatchRepository
{
	private readonly IDatabaseConnectionFactory _connectionFactory;

	public MatchRepository(IDatabaseConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public bool Delete(int id)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.Execute("DELETE FROM matches WHERE id = @Id", new { Id = id }) > 0;
		}
	}

	public Match Get(int id)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.QueryFirstOrDefault<Match>("SELECT * FROM matches WHERE id = @Id", new { Id = id });
		}
	}

	public IEnumerable<Match> GetAll()
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.Query<Match>("SELECT * FROM matches");
		}
	}

	public int Insert(Match entity)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.QueryFirst<int>("INSERT INTO matches (hometeamid, awayteamid, matchdate, matchtime, hometeamscore, awayteamscore, result, status) VALUES (@HomeTeamId, @AwayTeamId, @MatchDate, @MatchTime, @HomeTeamScore, @AwayTeamScore, @Result, @Status); SELECT SCOPE_IDENTITY();", entity);
		}
	}

	public bool Update(Match entity)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.Execute(@"
                UPDATE matches 
                SET 
                    hometeamid = @HomeTeamId, 
                    awayteamid = @AwayTeamId, 
                    matchdate = @MatchDate, 
                    matchtime = @MatchTime, 
                    hometeamscore = @HomeTeamScore, 
                    awayteamscore = @AwayTeamScore, 
                    result = @Result, 
                    status = @Status 
                WHERE id = @Id", entity) > 0;
		}
	}
}
