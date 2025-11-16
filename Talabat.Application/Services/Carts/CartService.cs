namespace Talabat.Application.Services;
public class CartService : ICartService
{
	private readonly IUnitOfWork _unitOfWork;
	public CartService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<CartResponse>> GetUserCartAsync(string userId, CancellationToken cancellationToken)
	{
		var cart = await _unitOfWork.Carts.GetUserCartAsync(userId, includeItems: true, withNoTracking: true, cancellationToken);

		if (cart is null)
			return Result.Failure<CartResponse>(CartErrors.CartNotFound);

		var response = new CartResponse(
			cart.Id,
			DateOnly.FromDateTime(cart.CreatedAt),
			cart.Items.Select(i => new CartItemResponse(
				i.Id,
				i.ProductId,
				i.Product.Name,
				i.Quantity,
				i.UnitPrice,
				i.Quantity * i.UnitPrice
			)),
			cart.Items.Sum(i => i.UnitPrice * i.Quantity)
		);

		return Result.Success(response);
	}

	public async Task<Result<CartResponse>> AddToCartAsync(string userId, AddToCartRequest request, CancellationToken cancellationToken)
	{
		var product = await _unitOfWork.Products.GetActiveProductByIdAsync(request.ProductId, cancellationToken);

		if (product is null)
			return Result.Failure<CartResponse>(ProductErrors.ProductNotFound);


		if (product.Quantity < request.Quantity)
			return Result.Failure<CartResponse>(ItemErrors.NotEnoughStock);

		var cart = await _unitOfWork.Carts.GetUserCartAsync(userId, includeItems: true, withNoTracking: false, cancellationToken);


		if (cart is null)
		{
			cart = new Cart { UserId = userId };
			await _unitOfWork.Carts.AddAsync(cart);
		}

		var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

		if (existingItem is not null)
		{
			if (product.Quantity < existingItem.Quantity + request.Quantity)
				return Result.Failure<CartResponse>(ItemErrors.NotEnoughStock);

			existingItem.Quantity += request.Quantity;
		}
		else
		{
			cart.Items.Add(new CartItem
			{
				ProductId = product.Id,
				Quantity = request.Quantity,
				UnitPrice = product.Price
			});
		}

		// Decrease product stock quantity
		product.Quantity -= request.Quantity;

		if (product.Quantity == 0)
			product.IsActive = false;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return await GetUserCartAsync(userId, cancellationToken);
	}

	public async Task<Result> RemoveItemAsync(string userId, int itemId, CancellationToken cancellationToken)
	{
		var cart = await _unitOfWork.Carts.GetUserCartAsync(userId, includeItems: true, withNoTracking: false, cancellationToken);

		if (cart is null)
			return Result.Failure(CartErrors.CartNotFound);

		var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

		if (item is null)
			return Result.Failure(ItemErrors.ItemNotFound);


		// Restore product stock quantity
		item.Product.Quantity += item.Quantity;

		if (item.Product.Quantity > 0)
			item.Product.IsActive = true;

		_unitOfWork.CartItems.Remove(item);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken)
	{
		var cart = await _unitOfWork.Carts.GetUserCartAsync(userId, includeItems: true, withNoTracking: false, cancellationToken);

		if (cart is null)
			return Result.Failure(CartErrors.CartNotFound);

		// Restore product stock quantities
		foreach (var item in cart.Items)
		{
			item.Product.Quantity += item.Quantity;

			if (item.Product.Quantity > 0)
				item.Product.IsActive = true;
		}

		_unitOfWork.CartItems.RemoveRange(cart.Items);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}