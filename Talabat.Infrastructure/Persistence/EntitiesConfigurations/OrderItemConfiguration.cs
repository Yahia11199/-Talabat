namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
	public void Configure(EntityTypeBuilder<OrderItem> builder)
	{
		builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
		builder.Property(x => x.ProductName).HasMaxLength(200);
	}
}
