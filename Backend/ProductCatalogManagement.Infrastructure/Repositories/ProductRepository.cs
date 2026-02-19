using Microsoft.EntityFrameworkCore;
using ProductCatalogManagement.DataContext;
using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Infrastructure.Repositories
{
    public class ProductRepository
        : Repository<ProductEntity>, IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProductEntity>> SearchAsync(
            string? name,
            Guid? categoryId)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p =>
                    p.Name.Contains(name));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
