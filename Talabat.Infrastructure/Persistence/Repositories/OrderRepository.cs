namespace Talabat.Infrastructure.Persistence.Repositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context) { }

	public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId, CancellationToken cancellationToken = default)
	{
		return await _context.Orders
			.Include(o => o.Items)
			.ThenInclude(i => i.Product)
			.Where(o => o.UserId == userId)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public async Task<Order?> GetOrderWithItemsAsync(int orderId, CancellationToken cancellationToken = default)
	{
		return await _context.Orders
			.Include(o => o.Items)
			.SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);
	}
}
