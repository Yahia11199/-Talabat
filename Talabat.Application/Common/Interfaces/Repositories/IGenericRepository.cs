using System.Linq.Expressions;

namespace Talabat.Application.Common.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
	IQueryable<T> Query();
	Task<IEnumerable<T>> GetAllAsync(bool withNoTracking = true, CancellationToken cancellationToken = default);
	Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(T entity, CancellationToken cancellationToken = default);
	void Update(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
	Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}