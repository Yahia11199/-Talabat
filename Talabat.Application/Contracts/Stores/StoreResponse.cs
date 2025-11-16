namespace Talabat.Application.Contracts;

public record StoreResponse(
	 int Id,
	 string Name,
	 string Description,
	 string Address,
	 string PhoneNumber,
	 string Category,
	 bool IsActive
);
public record StoreResponseV2(
	 int Id,
	 string Name,
	 string Description,
	 string Address,
	 string PhoneNumber,
	 string Category
);