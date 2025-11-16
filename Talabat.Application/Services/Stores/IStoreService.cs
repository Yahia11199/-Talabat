namespace Talabat.Application.Services;
public interface IStoreService
{
	Task<IEnumerable<StoreResponse>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<Result<PaginatedList<StoreResponse>>> GetAvailableV1(RequestFilters filters, CancellationToken cancellationToken = default);
	Task<Result<PaginatedList<StoreResponseV2>>> GetAvailableV2(RequestFilters filters, CancellationToken cancellationToken = default);
	Task<Result<StoreResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
	Task<Result<StoreResponse>> AddByOwnerAsync(string OwnerId, StoreRequest request, CancellationToken cancellationToken = default);
	Task<Result<StoreResponse>> AddByAdminAsync(AddStoreRequestByAdmin request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(string OwnerId, int id, StoreRequest request, CancellationToken cancellationToken = default);
	Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
	Task<Result> ToggleStatusAsync(int id, CancellationToken cancellationToken = default);
}
