namespace Talabat.Application.Common.Errors;
public record ItemErrors
{
	public static readonly Error ItemNotFound =
			new("Item.NotFound", "No Item was found with the given ID", StatusCodes.Status404NotFound);

	public static readonly Error InvalidQuantity =
		new("Item.InvalidQuantity", "Quantity must be greater than zero.", StatusCodes.Status400BadRequest);

	public static readonly Error NotEnoughStock =
		new("Item.NotEnoughStock", "Not enough stock available for this product.", StatusCodes.Status400BadRequest);

	public static readonly Error InsufficientStock =
		new("Item.InsufficientStock", "The requested quantity exceeds available stock.", StatusCodes.Status400BadRequest);
}
