using Microsoft.EntityFrameworkCore;
using TradeWind.Modules.Identity.Domain.Entities;

namespace TradeWind.Infrastructure.Database.DbContexts;

public class ApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}
}
