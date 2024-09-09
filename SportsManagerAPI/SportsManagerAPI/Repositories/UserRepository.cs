using Dapper;
using SportsManagerAPI.Database;
using SportsManagerAPI.Entities;
using SportsManagerAPI.Models;
using SportsManagerAPI.Repositories.Abstractions;

namespace SportsManagerAPI.Repositories;

public interface IUserRepository : IRepository<User>
{
	bool ValidateUser(User login);
}

public class UserRepository : IUserRepository
{
	private readonly IDatabaseConnectionFactory _connectionFactory;

	public UserRepository(IDatabaseConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public bool Delete(int id)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			return connection.Execute("DELETE FROM users WHERE id = @Id", new { Id = id }) > 0;
		}
	}

	public User Get(int id)
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.QueryFirstOrDefault<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
	}

	public IEnumerable<User> GetAll()
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.Query<User>("SELECT * FROM users");
	}

	public int Insert(User user)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			return connection.QueryFirst<int>(
				"INSERT INTO users (username, password, email, firstname, lastname, userroleid) " +
				"VALUES (@Username, @Password, @Email, @FirstName, @LastName, @UserRoleId); " +
				"SELECT SCOPE_IDENTITY();", user);
		}
	}

	public bool Update(User user)
	{
		using (var connection = _connectionFactory.CreateConnection())
			return connection.Execute("UPDATE users SET username = @Username, password = @Password, email = @Email, firstname = @FirstName, lastname = @LastName, userroleid = @UserRoleId WHERE id = @Id", user) > 0;
	}

	public bool ValidateUser(User login)
	{
		using (var connection = _connectionFactory.CreateConnection())
		{
			var user = connection.QueryFirstOrDefault<User>(
				"SELECT * FROM users WHERE Email = @Email",
				new { Email = login.Email});

			if (user == null)
				return false;

			// Hashlenmiş şifreyi doğrula
			return BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
		}
	}
}
