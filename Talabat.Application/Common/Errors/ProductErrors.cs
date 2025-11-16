namespace Talabat.Application.Common.Errors;

public record ProductErrors
{
	public static readonly Error ProductNotFound =
		new("Product.NotFound", "No Product was found with the given ID", StatusCodes.Status404NotFound);


	public static readonly Error DuplicatedProductName =
		new("Product.DuplicatedName", "Another product with the same Name in this store is already exists", StatusCodes.Status409Conflict);
}