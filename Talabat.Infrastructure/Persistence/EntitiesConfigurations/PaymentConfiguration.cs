namespace Talabat.Infrastructure.Persistence.EntitiesConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
	public void Configure(EntityTypeBuilder<Payment> builder)
	{

		builder.Property(p => p.Amount)
			   .HasColumnType("decimal(18,2)");

		builder.Property(p => p.Currency)
			   .HasMaxLength(3);

		builder.Property(p => p.PaymentMethod)
			   .HasMaxLength(50);

		builder.Property(p => p.TransactionId)
			   .HasMaxLength(100);

		builder.HasIndex(p => p.TransactionId)
			   .IsUnique();
	}
}
