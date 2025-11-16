using System.Linq.Expressions;

namespace Talabat.Infrastructure.Persistence.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	protected readonly ApplicationDbContext _context;
	protected readonly DbSet<T> _dbSet;
	public GenericRepository(ApplicationDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public IQueryable<T> Query() => _dbSet.AsQueryable();

	public async Task<IEnumerable<T>> GetAllAsync(bool withNoTracking = true, CancellationToken cancellationToken = default)
	{
		if (withNoTracking)
			_dbSet.AsNoTracking();

		return await _dbSet.ToListAsync(cancellationToken);
	}

	public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		=> await _dbSet.FindAsync(id, cancellationToken);

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
		=> await _dbSet.AddAsync(entity, cancellationToken);

	public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		=> await _dbSet.AnyAsync(predicate, cancellationToken);

	public void Update(T entity) => _dbSet.Update(entity);

	public void Remove(T entity) => _dbSet.Remove(entity);

	public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
}
