namespace Talabat.Application.Contracts;

public record AddToCartRequest(
	 int ProductId,
	 int Quantity
);