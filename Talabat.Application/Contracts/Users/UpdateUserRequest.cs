namespace Talabat.Application.Contracts;
public record UpdateUserRequest(
	string FirstName,
	string LastName,
	string Email,
	IList<string> Roles
);
