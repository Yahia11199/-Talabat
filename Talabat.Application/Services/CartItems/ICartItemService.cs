namespace Talabat.Application.Services;

public interface ICartItemService
{
	Task<Result<IEnumerable<CartItemResponse>>> GetAllItemsAsync(string userId, CancellationToken cancellationToken);
	Task<Result<CartItemResponse>> GetItemByIdAsync(string userId, int itemId, CancellationToken cancellationToken);
	Task<Result> UpdateQuantityAsync(string userId, int itemId, int quantity, CancellationToken cancellationToken);
}