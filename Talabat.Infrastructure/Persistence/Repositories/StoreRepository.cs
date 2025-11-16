namespace Talabat.Infrastructure.Persistence.Repositories;

public class StoreRepository : GenericRepository<Store>, IStoreRepository
{
	public StoreRepository(ApplicationDbContext context) : base(context) { }

	public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
		=> await _context.Stores.AnyAsync(s => s.Id == id, cancellationToken);

	public async Task<bool> IsOwnerAsync(int storeId, string ownerId, CancellationToken cancellationToken = default)
		=> await _context.Stores.AnyAsync(s => s.Id == storeId && s.OwnerId == ownerId, cancellationToken);
}
