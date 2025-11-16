namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
	public void Configure(EntityTypeBuilder<Cart> builder)
	{
		builder.Property(c => c.UserId).HasMaxLength(450);
	}
}