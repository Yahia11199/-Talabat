namespace Talabat.Application.Services;

public interface IProductService
{
	Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int storeId, CancellationToken cancellationToken = default);
	Task<Result<ProductResponse>> GetAsync(int storeId, int id, CancellationToken cancellationToken = default);
	Task<Result<ProductResponse>> AddAsync(string OwnerId, int storeId, ProductRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(string OwnerId, int storeId, int id, ProductRequest request, CancellationToken cancellationToken = default);
	Task<Result> DeleteAsync(string OwnerId, int storeId, int id, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusAsync(string OwnerId, int storeId, int id, CancellationToken cancellationToken = default);
}
