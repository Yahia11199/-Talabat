namespace Talabat.Application.Common.Errors;
public record OrderErrors
{
	public static readonly Error OrderNotFound =
	  new("Order.NotFound", "No order was found with the given ID", StatusCodes.Status404NotFound);

	public static readonly Error EmptyCart =
		new("Order.EmptyCart", "Cannot create an order because the cart is empty", StatusCodes.Status400BadRequest);
}
