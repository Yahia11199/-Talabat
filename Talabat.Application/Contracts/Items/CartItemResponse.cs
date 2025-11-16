namespace Talabat.Application.Contracts;
public record CartItemResponse(
	 int Id,
	 int ProductId,
	 string ProductName,
	 int Quantity,
	 decimal UnitPrice,
	 decimal Total
);
