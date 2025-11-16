namespace Talabat.Application.Contracts;
public record AddStoreRequestByAdmin(
	 string Name,
	 string Description,
	 string Address,
	 string PhoneNumber,
	 string Category,
	 string OwnerId
);

