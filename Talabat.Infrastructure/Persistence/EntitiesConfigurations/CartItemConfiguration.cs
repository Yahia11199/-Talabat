namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
	public void Configure(EntityTypeBuilder<CartItem> builder)
	{
		builder.Property(c => c.UnitPrice).HasColumnType("decimal(18,2)");
	}
}