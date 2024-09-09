using Dapper;
using SportsManagerAPI.Database;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Repositories.Abstractions;

namespace SportsManagerAPI.Repositories;

public interface ITeamRepository : IRepository<Team>
{
}
public class TeamRepository : ITeamRepository
{
	private readonly IDatabaseConnectionFactory _connectionFactory;

	public TeamRepository(IDatabaseConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public bool Delete(int Id)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.Execute("DELETE FROM teams WHERE id = @Id", new { id = Id }) > 0;
		}
	}

	public Team Get(int Id)
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.QueryFirstOrDefault<Team>("SELECT * FROM teams WHERE id = @Id", new { id = Id });
	}

	public IEnumerable<Team> GetAll()
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.Query<Team>("SELECT * FROM teams");
	}

	public int Insert(Team entity)
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.QueryFirst<int>("INSERT INTO teams (name, shortname, logo) VALUES (@Name, @ShortName, @Logo); SELECT SCOPE_IDENTITY();", entity);
	}

	public bool Update(Team entity)
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.Execute("UPDATE teams SET name = @Name, shortname = @ShortName, logo = @Logo WHERE id = @Id", entity) > 0;
	}

}
