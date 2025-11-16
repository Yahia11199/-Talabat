namespace Talabat.Application.Common.Errors;

public record StoreErrors
{
	public static readonly Error StoreNotFound =
	new("Store.NotFound", "No Store was found with the given ID", StatusCodes.Status404NotFound);

	public static readonly Error DuplicatedStoreName =
		new("Store.DuplicatedName", "Another Store with the same Name is already exists", StatusCodes.Status409Conflict);

	public static readonly Error Forbidden =
		new("Store.Forbidden", "You are not allowed to access this store.", StatusCodes.Status403Forbidden);
}
