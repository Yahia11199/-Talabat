using System.Linq.Dynamic.Core;

namespace Talabat.Application.Services;
public class StoreService : IStoreService
{
	private readonly IUnitOfWork _unitOfWork;
	public StoreService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<IEnumerable<StoreResponse>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var stores = await _unitOfWork.Stores.GetAllAsync(true, cancellationToken);

		return stores.Adapt<IEnumerable<StoreResponse>>();

	}

	public async Task<Result<PaginatedList<StoreResponse>>> GetAvailableV1(RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var query = _unitOfWork.Stores.Query()
			.Where(x => x.IsActive);


		if (!string.IsNullOrEmpty(filters.SearchValue))
		{
			query = query.Where(x => x.Name.Contains(filters.SearchValue));
		}


		if (!string.IsNullOrEmpty(filters.SortColumn))
		{
			query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
		}

		var source = query
			.ProjectToType<StoreResponse>()
			.AsNoTracking();


		var stores = await PaginatedList<StoreResponse>
			.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

		return Result.Success(stores);
	}

	public async Task<Result<PaginatedList<StoreResponseV2>>> GetAvailableV2(RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var query = _unitOfWork.Stores.Query()
			.Where(x => x.IsActive);


		if (!string.IsNullOrEmpty(filters.SearchValue))
		{
			query = query.Where(x => x.Name.Contains(filters.SearchValue));
		}


		if (!string.IsNullOrEmpty(filters.SortColumn))
		{
			query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
		}

		var source = query
			.ProjectToType<StoreResponseV2>()
			.AsNoTracking();


		var stores = await PaginatedList<StoreResponseV2>
			.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

		return Result.Success(stores);
	}

	public async Task<Result<StoreResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
	{
		var store = await _unitOfWork.Stores.GetByIdAsync(id, cancellationToken);

		return store is not null
			? Result.Success(store.Adapt<StoreResponse>())
			: Result.Failure<StoreResponse>(StoreErrors.StoreNotFound);
	}

	public async Task<Result<StoreResponse>> AddByOwnerAsync(string OwnerId, StoreRequest request, CancellationToken cancellationToken = default)
	{
		var isExistingStoreName = await _unitOfWork.Stores.AnyAsync(x => x.Name == request.Name, cancellationToken);

		if (isExistingStoreName)
			return Result.Failure<StoreResponse>(StoreErrors.DuplicatedStoreName);

		var store = request.Adapt<Store>();
		store.OwnerId = OwnerId;

		await _unitOfWork.Stores.AddAsync(store, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var storeResponse = store.Adapt<StoreResponse>();

		return Result.Success(storeResponse);
	}

	public async Task<Result<StoreResponse>> AddByAdminAsync(AddStoreRequestByAdmin request, CancellationToken cancellationToken = default)
	{
		var isExistingStoreName = await _unitOfWork.Stores.AnyAsync(x => x.Name == request.Name, cancellationToken);

		if (isExistingStoreName)
			return Result.Failure<StoreResponse>(StoreErrors.DuplicatedStoreName);

		var ownerExists = await _unitOfWork.Users.ExistsAsync(request.OwnerId, cancellationToken);

		if (!ownerExists)
			return Result.Failure<StoreResponse>(UserErrors.UserNotFound);

		var store = request.Adapt<Store>();

		await _unitOfWork.Stores.AddAsync(store, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var storeResponse = store.Adapt<StoreResponse>();

		return Result.Success(storeResponse);
	}

	public async Task<Result> UpdateAsync(string OwnerId, int id, StoreRequest request, CancellationToken cancellationToken = default)
	{
		if (await _unitOfWork.Stores.GetByIdAsync(id, cancellationToken) is not { } currentStore)
			return Result.Failure(StoreErrors.StoreNotFound);

		if (OwnerId != currentStore.OwnerId)
			return Result.Failure(StoreErrors.Forbidden);

		var isExistingStoreName = await _unitOfWork.Stores.AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);

		if (isExistingStoreName)
			return Result.Failure<StoreResponse>(StoreErrors.DuplicatedStoreName);

		currentStore = request.Adapt(currentStore);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var currentStore = await _unitOfWork.Stores.GetByIdAsync(id, cancellationToken);

		if (currentStore is null)
			return Result.Failure(StoreErrors.StoreNotFound);

		currentStore.IsActive = false;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result> ToggleStatusAsync(int id, CancellationToken cancellationToken = default)
	{
		var currentStore = await _unitOfWork.Stores.GetByIdAsync(id, cancellationToken);

		if (currentStore is null)
			return Result.Failure(StoreErrors.StoreNotFound);

		currentStore.IsActive = !currentStore.IsActive;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
