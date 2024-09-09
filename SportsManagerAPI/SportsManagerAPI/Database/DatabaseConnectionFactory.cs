using Microsoft.Data.SqlClient;

namespace SportsManagerAPI.Database;

public interface IDatabaseConnectionFactory
{
	SqlConnection CreateConnection();
}
public class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
	private readonly string _connectionString;
	public DatabaseConnectionFactory(string connectionSting)
	{
		_connectionString = connectionSting;
	}
	public SqlConnection CreateConnection()
	{
		var connection = new SqlConnection(_connectionString);
		try
		{
			connection.Open();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
		return connection;


	}
}