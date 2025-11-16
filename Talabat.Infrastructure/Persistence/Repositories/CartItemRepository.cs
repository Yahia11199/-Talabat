namespace Talabat.Infrastructure.Persistence.Repositories;
public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
{
	public CartItemRepository(ApplicationDbContext context) : base(context) { }

	public async Task<CartItem?> GetUserItemAsync(string userId, int itemId, CancellationToken cancellationToken = default)
	{
		return await _context.CartItems
			.Include(i => i.Product)
			.Include(i => i.Cart)
			.FirstOrDefaultAsync(i => i.Id == itemId && i.Cart.UserId == userId, cancellationToken);
	}

	public async Task<IEnumerable<CartItem>> GetUserItemsAsync(string userId, CancellationToken cancellationToken = default)
	{
		var cart = await _context.Carts
			.Include(c => c.Items)
			.ThenInclude(i => i.Product)
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);

		return cart?.Items ?? Enumerable.Empty<CartItem>();
	}
}
