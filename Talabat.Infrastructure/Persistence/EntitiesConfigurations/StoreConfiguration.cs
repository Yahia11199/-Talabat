namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
	public void Configure(EntityTypeBuilder<Store> builder)
	{
		builder.HasIndex(x => x.Name).IsUnique(); // index on Name 

		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.Description).HasMaxLength(1000);
		builder.Property(x => x.Address).HasMaxLength(250);
		builder.Property(x => x.PhoneNumber).HasMaxLength(15);
		builder.Property(x => x.Category).HasMaxLength(100);
		builder.Property(x => x.IsActive).HasDefaultValue(true);
	}
}