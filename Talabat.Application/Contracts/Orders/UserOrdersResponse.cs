namespace Talabat.Application.Contracts;

public record UserOrdersResponse(
	int Id,
	DateOnly CreatedAt,
	string Status,
	decimal TotalPrice,
	bool IsPaid,
	DateOnly? PaidAt,
	IEnumerable<OrderItemResponse> Items
);
