namespace Talabat.Application.Contracts;

public record RefreshTokenRequest(
	string Token,
	string RefreshToken
);
