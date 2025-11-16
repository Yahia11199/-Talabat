namespace Talabat.Domain.Entities;

public class Product : AuditableEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	public string Department { get; set; } = string.Empty;

	public int StoreId { get; set; }
	public Store Store { get; set; } = default!;
}
