namespace Talabat.Infrastructure.Persistence.Repositories;
public class CartRepository : GenericRepository<Cart>, ICartRepository
{
	public CartRepository(ApplicationDbContext context) : base(context)
	{

	}

	public async Task<Cart?> GetUserCartAsync(string userId, bool includeItems = false, bool withNoTracking = true, CancellationToken cancellationToken = default)
	{
		var query = _context.Carts.AsQueryable();

		if (includeItems)
		{
			query = query
				.Include(c => c.Items)
				.ThenInclude(i => i.Product);
		}

		if (withNoTracking)
			query = query.AsNoTracking();

		return await query
			.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
	}
}
