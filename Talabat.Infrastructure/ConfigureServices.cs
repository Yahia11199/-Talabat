using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Infrastructure.Persistence.Repositories;

namespace Talabat.Infrastructure;
public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
			builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
		});

		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

		// Unit of Work
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}