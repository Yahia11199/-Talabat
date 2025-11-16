namespace Talabat.Application.Contracts;

public record UserProfileResponse(
	string Email,
	string UserName,
	string FirstName,
	string LastName
);