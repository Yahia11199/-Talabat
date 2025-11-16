namespace Talabat.Application.Services;

public interface IOrderService
{
	Task<Result<IEnumerable<UserOrdersResponse>>> GetUserOrdersAsync(string userId, CancellationToken cancellationToken);
	Task<Result<OrderResponse>> CreateOrderAsync(string userId, CancellationToken cancellationToken);
	Task<Result> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, CancellationToken cancellationToken);
}