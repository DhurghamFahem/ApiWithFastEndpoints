using ApiWithFastEndpoints.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiWithFastEndpoints.Data;

// /Data/AppDbContext.cs
public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    // Other DbSets and configurations

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
