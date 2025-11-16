namespace Talabat.Api.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting(RateLimiters.IpLimiter)]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authorService;
	private readonly ILogger<AuthController> _logger;
	public AuthController(IAuthService authorService, ILogger<AuthController> logger)
	{
		_authorService = authorService;
		_logger = logger;
	}

	[HttpPost("")]
	public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Logging with email: {email} and password: {password}", request.Email, request.Password);

		var result = await _authorService.GetTokenAsync(request.Email, request.Password, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("refresh")]
	public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
	{
		var result = await _authorService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("revoke-refresh-token")]
	public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
	{
		var result = await _authorService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

		return result.IsSuccess ? Ok() : result.ToProblem();
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
	{
		var result = await _authorService.RegisterAsync(request, cancellationToken);

		return result.IsSuccess ? Ok() : result.ToProblem();
	}
}
