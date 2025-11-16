namespace Talabat.Application.Common.Interfaces.Repositories;
public interface ICartRepository : IGenericRepository<Cart>
{
	Task<Cart?> GetUserCartAsync(string userId, bool includeItems = false, bool withNoTracking = true, CancellationToken cancellationToken = default);
}
