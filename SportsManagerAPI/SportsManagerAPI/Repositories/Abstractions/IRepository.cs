namespace SportsManagerAPI.Repositories.Abstractions;

public interface IRepository<T>
{
	int Insert(T entity);
	bool Update(T entity);
	bool Delete(int id);
	T Get(int id);
	IEnumerable<T> GetAll();
}
