namespace Talabat.Application.Common.Mapping;
public class MappingConfigurations : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<Domain.Entities.Product, ProductResponse>()
		.Map(dest => dest.UnitPrice, src => src.Price);

		config.NewConfig<(ApplicationUser user, IList<string> roles), UserResponse>()
		.Map(dest => dest, src => src.user)
		.Map(dest => dest.Roles, src => src.roles);

		config.NewConfig<CreateUserRequest, ApplicationUser>()
		.Map(dest => dest.UserName, src => src.Email);

		config.NewConfig<UpdateUserRequest, ApplicationUser>()
		.Map(dest => dest.UserName, src => src.Email)
		.Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());
	}
}
