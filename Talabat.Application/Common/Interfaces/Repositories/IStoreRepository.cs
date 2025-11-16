namespace Talabat.Application.Common.Interfaces.Repositories;
public interface IStoreRepository : IGenericRepository<Store>
{
	Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
	Task<bool> IsOwnerAsync(int storeId, string ownerId, CancellationToken cancellationToken);
}