namespace Talabat.Application.Contracts;

public record OrderResponse(
	int Id,
	DateOnly CreatedAt,
	string Status,
	decimal TotalPrice,
	IEnumerable<OrderItemResponse> Items
);
