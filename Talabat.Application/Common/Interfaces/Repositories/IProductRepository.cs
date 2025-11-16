namespace Talabat.Application.Common.Interfaces.Repositories;
public interface IProductRepository : IGenericRepository<Domain.Entities.Product>
{
	Task<bool> ExistsByNameAsync(string name, int storeId, CancellationToken cancellationToken = default);
	Task<Domain.Entities.Product?> GetByIdAndStoreAsync(int id, int storeId, CancellationToken cancellationToken = default);
	Task<bool> ExistsByproductName(int id, int storeId, string productName, CancellationToken cancellationToken = default);
	Task<Domain.Entities.Product?> GetActiveProductByIdAsync(int id, CancellationToken cancellationToken = default);

}