namespace Talabat.Domain.Entities;

public class Cart
{
	public int Id { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public string UserId { get; set; } = string.Empty;
	public ApplicationUser User { get; set; } = default!;
	public ICollection<CartItem> Items { get; set; } = [];
	public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);
}