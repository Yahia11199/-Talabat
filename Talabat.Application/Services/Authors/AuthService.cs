namespace Talabat.Application.Services;
public class AuthService : IAuthService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IJwtProvider _jwtProvider;
	private readonly SignInManager<ApplicationUser> _signInManager;
	public AuthService(UserManager<ApplicationUser> userManager,
		IJwtProvider jwtProvider,
		SignInManager<ApplicationUser> signInManager)
	{
		_userManager = userManager;
		_jwtProvider = jwtProvider;
		_signInManager = signInManager;
	}

	private readonly int _refreshTokenExpiryDays = 14;
	public async Task<Result<AuthorResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
	{
		if (await _userManager.FindByEmailAsync(email) is not { } user)
			return Result.Failure<AuthorResponse>(UserErrors.InvalidCredentials);

		if (user.IsDisabled)
			return Result.Failure<AuthorResponse>(UserErrors.DisabledUser);

		var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

		if (result.Succeeded)
		{
			var userRoles = await _userManager.GetRolesAsync(user);

			var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
			var refreshToken = GenerateRefreshToken();
			var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

			user.RefreshTokens.Add(new RefreshToken
			{
				Token = refreshToken,
				ExpiresOn = refreshTokenExpiration
			});

			await _userManager.UpdateAsync(user);

			var response = new AuthorResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn, refreshToken, refreshTokenExpiration);

			return Result.Success(response);
		}

		var error = result.IsLockedOut
			? UserErrors.LockedUser
			: UserErrors.InvalidCredentials;

		return Result.Failure<AuthorResponse>(error);
	}

	public async Task<Result<AuthorResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateToken(token);

		if (userId is null)
			return Result.Failure<AuthorResponse>(UserErrors.InvalidJwtToken);

		if (await _userManager.FindByIdAsync(userId) is not { } user)
			return Result.Failure<AuthorResponse>(UserErrors.InvalidJwtToken);

		if (user.IsDisabled)
			return Result.Failure<AuthorResponse>(UserErrors.DisabledUser);

		if (user.LockoutEnd > DateTime.UtcNow)
			return Result.Failure<AuthorResponse>(UserErrors.LockedUser);

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken && t.IsActive);

		if (userRefreshToken is null)
			return Result.Failure<AuthorResponse>(UserErrors.InvalidJwtToken);

		userRefreshToken.RevokedOn = DateTime.UtcNow;

		var userRoles = await _userManager.GetRolesAsync(user);

		var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);

		var newRefreshToken = GenerateRefreshToken();

		var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

		user.RefreshTokens.Add(new RefreshToken
		{
			Token = newRefreshToken,
			ExpiresOn = refreshTokenExpiration
		});

		await _userManager.UpdateAsync(user);

		var response = new AuthorResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);

		return Result.Success(response);
	}

	public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
	{
		var userId = _jwtProvider.ValidateToken(token);

		if (userId is null)
			return Result.Failure(UserErrors.InvalidJwtToken);

		if (await _userManager.FindByIdAsync(userId) is not { } user)
			return Result.Failure(UserErrors.InvalidJwtToken);

		var userRefreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken && t.IsActive);

		if (userRefreshToken is null)
			return Result.Failure(UserErrors.InvalidRefreshToken);

		userRefreshToken.RevokedOn = DateTime.UtcNow;

		await _userManager.UpdateAsync(user);

		return Result.Success();
	}

	public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExists = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

		if (emailIsExists)
			return Result.Failure<AuthorResponse>(UserErrors.DuplicatedEmail);

		var user = request.Adapt<ApplicationUser>();
		user.UserName = request.Email;

		var result = await _userManager.CreateAsync(user, request.Password);

		if (result.Succeeded)
		{
			//Add Default Member Role
			await _userManager.AddToRoleAsync(user, DefaultRoles.Member.Name);

			return Result.Success();
		}

		var error = result.Errors.First();

		return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
	}

	private static string GenerateRefreshToken()
	{
		var refreshToken = RandomNumberGenerator.GetBytes(64);

		return Convert.ToBase64String(refreshToken);
	}
}