namespace Talabat.Infrastructure.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private readonly UserManager<ApplicationUser> _userManager;
	public UnitOfWork(ApplicationDbContext context,
		UserManager<ApplicationUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	public IStoreRepository Stores => new StoreRepository(_context);
	public IUserRepository Users => new UserRepository(_userManager);
	public IProductRepository Products => new ProductRepository(_context);
	public ICartRepository Carts => new CartRepository(_context);
	public ICartItemRepository CartItems => new CartItemRepository(_context);
	public IOrderRepository Orders => new OrderRepository(_context);
	public IGenericRepository<Payment> Payments => new GenericRepository<Payment>(_context);

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		=> await _context.SaveChangesAsync(cancellationToken);
}

