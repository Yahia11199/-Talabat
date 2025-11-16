using Microsoft.Extensions.Caching.Hybrid;

namespace Talabat.Application.Services;

public class ProductService : IProductService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly HybridCache _hybridCache;

	private const string _cachePrefix = "availableProducts";

	public ProductService(IUnitOfWork unitOfWork, HybridCache hybridCache)
	{
		_unitOfWork = unitOfWork;
		_hybridCache = hybridCache;
	}

	public async Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(int storeId, CancellationToken cancellationToken = default)
	{
		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<IEnumerable<ProductResponse>>(StoreErrors.StoreNotFound);

		var cacheKey = $"{_cachePrefix}-{storeId}";


		var response = await _hybridCache.GetOrCreateAsync<IEnumerable<ProductResponse>>(
			cacheKey,

			async cacheEntry =>
			{
				return await _unitOfWork.Products.Query()
							.Where(x => x.StoreId == storeId && x.IsActive)
							.AsNoTracking()
							.ProjectToType<ProductResponse>()
							.ToListAsync(cancellationToken);
			},
			cancellationToken: cancellationToken
		);


		return Result.Success(response);
	}

	public async Task<Result<ProductResponse>> GetAsync(int storeId, int id, CancellationToken cancellationToken = default)
	{
		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<ProductResponse>(StoreErrors.StoreNotFound);

		var product = await _unitOfWork.Products.GetByIdAndStoreAsync(id, storeId, cancellationToken);

		return product is not null
			? Result.Success(product.Adapt<ProductResponse>())
			: Result.Failure<ProductResponse>(ProductErrors.ProductNotFound);
	}

	public async Task<Result<ProductResponse>> AddAsync(string OwnerId, int storeId, ProductRequest request, CancellationToken cancellationToken = default)
	{
		var isStoreOwner = await ValidateStoreOwnerAsync(storeId, OwnerId, cancellationToken);

		if (!isStoreOwner)
			return Result.Failure<ProductResponse>(StoreErrors.Forbidden);

		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<ProductResponse>(StoreErrors.StoreNotFound);


		var isExistingProductName = await _unitOfWork.Products.ExistsByNameAsync(request.Name, storeId, cancellationToken);

		if (isExistingProductName)
			return Result.Failure<ProductResponse>(ProductErrors.DuplicatedProductName);

		var product = request.Adapt<Domain.Entities.Product>();
		product.StoreId = storeId;

		await _unitOfWork.Products.AddAsync(product, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var response = product.Adapt<ProductResponse>();

		await _hybridCache.RemoveAsync($"{_cachePrefix}-{storeId}", cancellationToken);

		return Result.Success(response);
	}

	public async Task<Result> UpdateAsync(string OwnerId, int storeId, int id, ProductRequest request, CancellationToken cancellationToken = default)
	{
		var isStoreOwner = await ValidateStoreOwnerAsync(storeId, OwnerId, cancellationToken);

		if (!isStoreOwner)
			return Result.Failure<ProductResponse>(StoreErrors.Forbidden);

		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<ProductResponse>(StoreErrors.StoreNotFound);


		var productNameIsExists = await _unitOfWork.Products
			.ExistsByproductName(id, storeId, request.Name, cancellationToken);

		if (productNameIsExists)
			return Result.Failure<ProductResponse>(ProductErrors.DuplicatedProductName);


		var product = await _unitOfWork.Products.GetByIdAndStoreAsync(id, storeId, cancellationToken);

		if (product is null)
			return Result.Failure(ProductErrors.ProductNotFound);

		product = request.Adapt(product);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		await _hybridCache.RemoveAsync($"{_cachePrefix}-{storeId}", cancellationToken);

		return Result.Success();
	}

	public async Task<Result> DeleteAsync(string OwnerId, int storeId, int id, CancellationToken cancellationToken = default)
	{
		var isStoreOwner = await ValidateStoreOwnerAsync(storeId, OwnerId, cancellationToken);

		if (!isStoreOwner)
			return Result.Failure<ProductResponse>(StoreErrors.Forbidden);

		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<ProductResponse>(StoreErrors.StoreNotFound);

		var product = await _unitOfWork.Products.GetByIdAndStoreAsync(id, storeId, cancellationToken);

		if (product is null)
			return Result.Failure(ProductErrors.ProductNotFound);

		product.IsActive = false;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		await _hybridCache.RemoveAsync($"{_cachePrefix}-{storeId}", cancellationToken);

		return Result.Success();
	}

	public async Task<Result> ToggleStatusAsync(string OwnerId, int storeId, int id, CancellationToken cancellationToken = default)
	{
		var isStoreOwner = await ValidateStoreOwnerAsync(storeId, OwnerId, cancellationToken);

		if (!isStoreOwner)
			return Result.Failure<ProductResponse>(StoreErrors.Forbidden);

		var storeIsExists = await ValidateStoreExistsAsync(storeId, cancellationToken);

		if (!storeIsExists)
			return Result.Failure<ProductResponse>(StoreErrors.StoreNotFound);

		var product = await _unitOfWork.Products.GetByIdAndStoreAsync(id, storeId, cancellationToken);

		if (product is null)
			return Result.Failure(ProductErrors.ProductNotFound);

		product.IsActive = !product.IsActive;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		await _hybridCache.RemoveAsync($"{_cachePrefix}-{storeId}", cancellationToken);

		return Result.Success();
	}

	private async Task<bool> ValidateStoreExistsAsync(int storeId, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Stores.ExistsAsync(storeId, cancellationToken);
	}

	private async Task<bool> ValidateStoreOwnerAsync(int storeId, string OwnerId, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Stores.IsOwnerAsync(storeId, OwnerId, cancellationToken);
	}
}