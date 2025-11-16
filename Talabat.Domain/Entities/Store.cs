namespace Talabat.Domain.Entities;
public class Store : AuditableEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
	public string Category { get; set; } = string.Empty;
	public string OwnerId { get; set; } = string.Empty;
	public ICollection<Product> Products { get; set; } = [];
}
