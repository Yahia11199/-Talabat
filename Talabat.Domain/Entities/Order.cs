namespace Talabat.Domain.Entities;
public class Order
{
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;
	public ApplicationUser User { get; set; } = default!;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public decimal TotalPrice { get; set; }
	public OrderStatus Status { get; set; } = OrderStatus.Pending;
	public ICollection<OrderItem> Items { get; set; } = [];
	public bool IsPaid { get; set; } = false;
	public DateTime? PaidAt { get; set; }
}
