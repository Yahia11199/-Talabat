namespace Talabat.Application.Contracts;

public record ProductResponse(
	int Id,
	string Name,
	string Description,
	decimal UnitPrice,
	string Department,
	int StoreId,
	int Quantity
);