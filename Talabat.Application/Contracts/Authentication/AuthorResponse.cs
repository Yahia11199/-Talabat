namespace Talabat.Application.Contracts;
public record AuthorResponse(
	string Id,
	string? Email,
	string FirstName,
	string LastName,
	string Token,
	int ExpiresIn,
	string RefreshToken,
	DateTime RefreshTokenExpiration
);