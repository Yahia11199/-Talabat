namespace Talabat.Application.Common.Interfaces.Repositories;
public interface ICartItemRepository : IGenericRepository<CartItem>
{
	Task<CartItem?> GetUserItemAsync(string userId, int itemId, CancellationToken cancellationToken = default);
	Task<IEnumerable<CartItem>> GetUserItemsAsync(string userId, CancellationToken cancellationToken = default);
}
