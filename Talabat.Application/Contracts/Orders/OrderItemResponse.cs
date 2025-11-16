namespace Talabat.Application.Contracts;

public record OrderItemResponse(
	int ProductId,
	string ProductName,
	int Quantity,
	decimal UnitPrice,
	decimal Total
);