namespace Talabat.Domain.Entities;

public class Payment
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; } = default!;
	public decimal Amount { get; set; }
	public string Currency { get; set; } = string.Empty;
	public string PaymentMethod { get; set; } = string.Empty;
	public string TransactionId { get; set; } = string.Empty;
	public bool IsSuccess { get; set; }
	public DateTime PaidAt { get; set; } = DateTime.UtcNow;
}
