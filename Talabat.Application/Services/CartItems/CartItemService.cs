namespace Talabat.Application.Services;

public class CartItemService : ICartItemService
{
	private readonly IUnitOfWork _unitOfWork;
	public CartItemService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<CartItemResponse>>> GetAllItemsAsync(string userId, CancellationToken cancellationToken)
	{
		var Items = await _unitOfWork.CartItems.GetUserItemsAsync(userId, cancellationToken);

		if (Items is null)
			return Result.Failure<IEnumerable<CartItemResponse>>(CartErrors.CartNotFound);

		var response = Items.Select(i => new CartItemResponse(
			i.Id,
			i.ProductId,
			i.Product.Name,
			i.Quantity,
			i.UnitPrice,
			i.Quantity * i.UnitPrice
		));

		return Result.Success(response);
	}

	public async Task<Result<CartItemResponse>> GetItemByIdAsync(string userId, int itemId, CancellationToken cancellationToken)
	{
		var item = await _unitOfWork.CartItems.GetUserItemAsync(userId, itemId, cancellationToken);

		if (item is null)
			return Result.Failure<CartItemResponse>(ItemErrors.ItemNotFound);

		var response = new CartItemResponse(
			item.Id,
			item.ProductId,
			item.Product.Name,
			item.Quantity,
			item.UnitPrice,
			item.Quantity * item.UnitPrice
		);

		return Result.Success(response);
	}

	public async Task<Result> UpdateQuantityAsync(string userId, int itemId, int quantity, CancellationToken cancellationToken)
	{
		var item = await _unitOfWork.CartItems.GetUserItemAsync(userId, itemId, cancellationToken);


		if (item is null)
			return Result.Failure(ItemErrors.ItemNotFound);

		int difference = quantity - item.Quantity;

		if (difference > 0)
		{
			if (difference > item.Product.Quantity)
				return Result.Failure(ItemErrors.InsufficientStock);

			item.Product.Quantity -= difference;
		}
		else if (difference < 0)
		{
			item.Product.Quantity += Math.Abs(difference);
		}

		item.Quantity = quantity;

		item.Product.IsActive = item.Product.Quantity > 0;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}