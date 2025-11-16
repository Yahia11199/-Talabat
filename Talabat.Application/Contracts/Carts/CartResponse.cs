namespace Talabat.Application.Contracts;
public record CartResponse(
	 int Id,
	 DateOnly CreatedAt,
	 IEnumerable<CartItemResponse> Items,
	 decimal TotalPrice
);
