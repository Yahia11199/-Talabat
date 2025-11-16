using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace Talabat.Api;
public static class DependencyInjection
{
	public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddControllers();

		services.AddHybridCache();

		services.AddCors(options =>
			  options.AddDefaultPolicy(builder =>
				   builder
				   .AllowAnyMethod()
				   .AllowAnyHeader()
				   .AllowAnyOrigin()
			  )
		);

		services.AddAuthConfig(configuration);

		var connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		services.AddOpenApiConfig();

		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		var stripeKey = configuration["Stripe:SecretKey"];
		if (!string.IsNullOrWhiteSpace(stripeKey))
		{
			Stripe.StripeConfiguration.ApiKey = stripeKey;
		}

		services.AddHealthChecks()
			.AddSqlServer(name: "database", connectionString: configuration.GetConnectionString("DefaultConnection")!);

		services.AddRateLimitingConfig();

		services.AddApiVersioning(options =>
		{
			options.DefaultApiVersion = new ApiVersion(1);
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.ReportApiVersions = true;
			options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
		})
		.AddApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'V";
			options.SubstituteApiVersionInUrl = true;
		});


		return services;
	}

	private static IServiceCollection AddOpenApiConfig(this IServiceCollection services)
	{
		services.AddOpenApi();
		return services;
	}

	private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddIdentity<ApplicationUser, ApplicationRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>();

		services.AddSingleton<IJwtProvider, JwtProvider>();

		services.AddOptions<JwtOptions>()
			.BindConfiguration(JwtOptions.SectionName)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(o =>
		{
			o.SaveToken = true;
			o.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.key!)),
				ValidIssuer = jwtSettings?.Issuer,
				ValidAudience = jwtSettings?.Audience,
			};
		});

		services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequiredLength = 8;
			//options.SignIn.RequireConfirmedEmail = true;
			options.User.RequireUniqueEmail = true;
		});

		return services;
	}

	private static IServiceCollection AddRateLimitingConfig(this IServiceCollection services)
	{
		services.AddRateLimiter(rateLimiterOptions =>
		{
			rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

			rateLimiterOptions.AddPolicy(RateLimiters.IpLimiter, httpContext =>
				RateLimitPartition.GetFixedWindowLimiter(

					partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
					factory: _ => new FixedWindowRateLimiterOptions
					{
						PermitLimit = 5,
						Window = TimeSpan.FromSeconds(20)
					}
				)
			);

			rateLimiterOptions.AddPolicy(RateLimiters.UserLimiter, httpContext =>
				RateLimitPartition.GetFixedWindowLimiter(

					partitionKey: httpContext.User.GetUserId(),
					factory: _ => new FixedWindowRateLimiterOptions
					{
						PermitLimit = 5,
						Window = TimeSpan.FromSeconds(20)
					}
				)
			);

			rateLimiterOptions.AddConcurrencyLimiter(RateLimiters.Concurrency, options =>
			{
				options.PermitLimit = 1000;
				options.QueueLimit = 100;
				options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
			});
		});

		return services;
	}

}