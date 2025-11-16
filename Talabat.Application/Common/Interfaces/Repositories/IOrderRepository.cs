namespace Talabat.Application.Common.Interfaces.Repositories;
public interface IOrderRepository : IGenericRepository<Order>
{
	Task<IEnumerable<Order>> GetUserOrdersAsync(string userId, CancellationToken cancellationToken = default);
	Task<Order?> GetOrderWithItemsAsync(int orderId, CancellationToken cancellationToken = default);
}
