using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using Talabat.Application.Services;
using ProductService = Talabat.Application.Services.ProductService;

namespace Talabat.Application;
public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IStoreService, StoreService>();
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICartService, CartService>();
		services.AddScoped<ICartItemService, CartItemService>();
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IPaymentService, PaymentService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IRoleService, RoleService>();


		services.AddMapsterConfig();
		services.AddFluentValidationConfig();

		return services;
	}

	private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
	{
		//add Mapster
		var mappingCong = TypeAdapterConfig.GlobalSettings;
		mappingCong.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton<IMapper>(new Mapper(mappingCong));

		return services;
	}

	private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddFluentValidationAutoValidation();
		return services;
	}
}
