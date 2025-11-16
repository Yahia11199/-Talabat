namespace Talabat.Application.Contracts;

public record RegisterRequest(
	string Email,
	string Password,
	string FirstName,
	string LastName
);