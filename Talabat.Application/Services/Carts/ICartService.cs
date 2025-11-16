namespace Talabat.Application.Services;
public interface ICartService
{
	Task<Result<CartResponse>> GetUserCartAsync(string userId, CancellationToken cancellationToken);
	Task<Result<CartResponse>> AddToCartAsync(string userId, AddToCartRequest request, CancellationToken cancellationToken);
	Task<Result> RemoveItemAsync(string userId, int itemId, CancellationToken cancellationToken);
	Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken);
}
