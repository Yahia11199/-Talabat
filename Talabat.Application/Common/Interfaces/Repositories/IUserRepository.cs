namespace Talabat.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
	Task<ApplicationUser?> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
	Task<bool> ExistsAsync(string userId, CancellationToken cancellationToken = default);
}

