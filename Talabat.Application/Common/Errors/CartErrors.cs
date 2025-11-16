namespace Talabat.Application.Common.Errors;

public record CartErrors
{
	public static readonly Error CartNotFound =
			new("Cart.NotFound", "Cart not found for this user.", StatusCodes.Status404NotFound);

}
