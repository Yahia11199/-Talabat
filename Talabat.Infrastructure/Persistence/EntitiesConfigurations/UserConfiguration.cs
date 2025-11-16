namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(x => x.FirstName).HasMaxLength(100);
		builder.Property(x => x.LastName).HasMaxLength(100);

		builder
		.OwnsMany(x => x.RefreshTokens)
		.ToTable("RefreshTokens")
		.WithOwner()
		.HasForeignKey("UserId");

		//var passwordHasher = new PasswordHasher<ApplicationUser>();

		// Default Data
		builder.HasData(new ApplicationUser
		{
			Id = DefaultUsers.Admin.Id,
			FirstName = DefaultUsers.Admin.FirstName,
			LastName = DefaultUsers.Admin.LastName,
			UserName = DefaultUsers.Admin.Email,
			NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
			Email = DefaultUsers.Admin.Email,
			NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
			SecurityStamp = DefaultUsers.Admin.SecurityStamp,
			ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
			EmailConfirmed = true,
			//PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.Admin.PasswordHash)
			PasswordHash = DefaultUsers.Admin.PasswordHash
		});
	}
}