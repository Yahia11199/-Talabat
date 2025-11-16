namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasIndex(x => new { x.StoreId, x.Name }).IsUnique();

		builder.Property(x => x.Name).HasMaxLength(100);
		builder.Property(x => x.Description).HasMaxLength(1000);
		builder.Property(x => x.Department).HasMaxLength(100);
		builder.Property(x => x.IsActive).HasDefaultValue(true);
		builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
	}
}