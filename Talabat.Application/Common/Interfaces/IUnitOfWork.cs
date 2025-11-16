namespace Talabat.Application.Common.Interfaces;
public interface IUnitOfWork
{
	IStoreRepository Stores { get; }
	IUserRepository Users { get; }
	IProductRepository Products { get; }
	ICartRepository Carts { get; }
	ICartItemRepository CartItems { get; }
	IOrderRepository Orders { get; }
	IGenericRepository<Payment> Payments { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
