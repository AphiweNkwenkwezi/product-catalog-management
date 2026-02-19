using Microsoft.EntityFrameworkCore;
using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
    }
}