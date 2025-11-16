
namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
	}
}
