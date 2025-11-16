namespace Talabat.Infrastructure.Persistence.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
	public ProductRepository(ApplicationDbContext context) : base(context) { }

	public async Task<bool> ExistsByNameAsync(string name, int storeId, CancellationToken cancellationToken = default) =>
		 await _context.Products.AnyAsync(x => x.Name == name && x.StoreId == storeId, cancellationToken);

	public async Task<Product?> GetByIdAndStoreAsync(int id, int storeId, CancellationToken cancellationToken = default) =>
		 await _context.Products.FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId, cancellationToken);

	public async Task<bool> ExistsByproductName(int id, int storeId, string productName, CancellationToken cancellationToken = default) =>
		await _context.Products.AnyAsync(x => x.StoreId == storeId && x.Id != id && x.Name == productName, cancellationToken);

	public async Task<Product?> GetActiveProductByIdAsync(int id, CancellationToken cancellationToken = default) =>
		  await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
}
