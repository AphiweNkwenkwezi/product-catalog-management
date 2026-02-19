using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.Application.Products
{
    public static class ProductQueryableExtensions
    {
        public static IEnumerable<ProductEntity> FilterByCategory(
            this IEnumerable<ProductEntity> products,
            IEnumerable<Guid> categoryIds)
        {
            return products.Where(p => 
                p.CategoryId.HasValue &&
                categoryIds.Contains(p.CategoryId.Value));
        }

        public static IEnumerable<ProductEntity> Paginate(
            this IEnumerable<ProductEntity> products,
            int page,
            int pageSize)
        {
            return products
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
