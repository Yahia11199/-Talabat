namespace Talabat.Application.Services;

public class OrderService : IOrderService
{
	private readonly IUnitOfWork _unitOfWork;
	public OrderService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<IEnumerable<UserOrdersResponse>>> GetUserOrdersAsync(string userId, CancellationToken cancellationToken)
	{
		var orders = await _unitOfWork.Orders.GetUserOrdersAsync(userId, cancellationToken);

		var response = orders.Select(order => new UserOrdersResponse(
			   order.Id,
			   DateOnly.FromDateTime(order.CreatedAt),
			   order.Status.ToString(),
			   order.TotalPrice,
			   order.IsPaid,
			   order.PaidAt.HasValue ? DateOnly.FromDateTime(order.PaidAt.Value) : null,
			   order.Items.Select(item => new OrderItemResponse(
				   item.ProductId,
				   item.Product.Name,
				   item.Quantity,
				   item.UnitPrice,
				   item.Quantity * item.UnitPrice
			   ))
		   ));

		return Result.Success(response);
	}

	public async Task<Result<OrderResponse>> CreateOrderAsync(string userId, CancellationToken cancellationToken)
	{
		var cart = await _unitOfWork.Carts.GetUserCartAsync(userId, includeItems: true, withNoTracking: false, cancellationToken);

		if (cart is null || cart.Items.Count == 0)
			return Result.Failure<OrderResponse>(OrderErrors.EmptyCart);

		var order = new Order
		{
			UserId = userId,
			CreatedAt = DateTime.UtcNow,
			Status = OrderStatus.Pending,
			TotalPrice = cart.Items.Sum(i => i.Quantity * i.UnitPrice),
			Items = cart.Items.Select(i => new OrderItem
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				UnitPrice = i.UnitPrice
			})
			.ToList()
		};

		await _unitOfWork.Orders.AddAsync(order);

		_unitOfWork.CartItems.RemoveRange(cart.Items);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var response = new OrderResponse(
			order.Id,
			DateOnly.FromDateTime(order.CreatedAt),
			order.Status.ToString(),
			order.TotalPrice,
			order.Items.Select(i => new OrderItemResponse(
				i.ProductId,
				i.Product.Name,
				i.Quantity,
				i.UnitPrice,
				i.Quantity * i.UnitPrice
			))
		);

		return Result.Success(response);
	}

	public async Task<Result> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus, CancellationToken cancellationToken)
	{
		var order = await _unitOfWork.Orders.GetByIdAsync(orderId, cancellationToken);

		if (order is null)
			return Result.Failure(OrderErrors.OrderNotFound);

		order.Status = newStatus;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}