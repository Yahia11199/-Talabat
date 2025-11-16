namespace Talabat.Application.Common.Interfaces;
public interface IApplicationDbContext
{
	public DbSet<Store> Stores { set; get; }
	public DbSet<Domain.Entities.Product> Products { set; get; }
	public DbSet<Cart> Carts { set; get; }
	public DbSet<CartItem> CartItems { set; get; }
	public DbSet<Order> Orders { set; get; }
	public DbSet<OrderItem> OrderItems { set; get; }
	public DbSet<Payment> Payments { get; set; }

	DbSet<ApplicationUser> Users { get; set; }
	DbSet<ApplicationRole> Roles { get; set; }
	DbSet<IdentityUserRole<string>> UserRoles { get; set; }
	DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
