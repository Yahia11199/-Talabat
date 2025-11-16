using System.Reflection;
using System.Security.Claims;

namespace Talabat.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
	: IdentityDbContext<ApplicationUser, ApplicationRole, string>(options), IApplicationDbContext
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		var cascadeFKs = modelBuilder.Model
		.GetEntityTypes()
		.SelectMany(t => t.GetForeignKeys())
		.Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

		foreach (var fk in cascadeFKs)
			fk.DeleteBehavior = DeleteBehavior.Restrict;

		base.OnModelCreating(modelBuilder);
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker.Entries<AuditableEntity>();

		foreach (var entityEntry in entries)
		{
			var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

			if (entityEntry.State == EntityState.Added)
			{
				entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
			}
			else if (entityEntry.State == EntityState.Modified)
			{
				entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
				entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}

	public DbSet<Store> Stores { set; get; }
	public DbSet<Product> Products { set; get; }
	public DbSet<Cart> Carts { set; get; }
	public DbSet<CartItem> CartItems { set; get; }
	public DbSet<Order> Orders { set; get; }
	public DbSet<OrderItem> OrderItems { set; get; }
	public DbSet<Payment> Payments { get; set; }
}