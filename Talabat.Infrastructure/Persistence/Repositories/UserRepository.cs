namespace Talabat.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
	private readonly UserManager<ApplicationUser> _userManager;
	public UserRepository(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<ApplicationUser?> GetByIdAsync(string userId, CancellationToken cancellationToken = default) =>
		 await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

	public async Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken = default) =>
		 await _userManager.Users.AnyAsync(u => u.Id == userId, cancellationToken);

}