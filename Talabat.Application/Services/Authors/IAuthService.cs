namespace Talabat.Application.Services;

public interface IAuthService
{
	Task<Result<AuthorResponse>> GetTokenAsync(string email, string Password, CancellationToken cancellationToken = default);
	Task<Result<AuthorResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
	Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
	Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

}
