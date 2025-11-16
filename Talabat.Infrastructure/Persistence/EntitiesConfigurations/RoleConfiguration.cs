namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
	public void Configure(EntityTypeBuilder<ApplicationRole> builder)
	{
		// Default Data
		builder.HasData([
		  new ApplicationRole
		  {
			  Id = DefaultRoles.Admin.Id,
			  Name = DefaultRoles.Admin.Name,
			  NormalizedName = DefaultRoles.Admin.Name.ToUpper(),
			  ConcurrencyStamp = DefaultRoles.Admin.ConcurrencyStamp
		  },
		  new ApplicationRole
		  {
			  Id = DefaultRoles.Owner.Id,
			  Name = DefaultRoles.Owner.Name,
			  NormalizedName = DefaultRoles.Owner.Name.ToUpper(),
			  ConcurrencyStamp = DefaultRoles.Owner.ConcurrencyStamp
		  },
		  new ApplicationRole
		  {
			  Id = DefaultRoles.Member.Id,
			  Name = DefaultRoles.Member.Name,
			  NormalizedName = DefaultRoles.Member.Name.ToUpper(),
			  ConcurrencyStamp = DefaultRoles.Member.ConcurrencyStamp,
			  IsDefault = true
		  }
		]);
	}
}