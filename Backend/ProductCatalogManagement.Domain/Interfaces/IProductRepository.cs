using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.Domain.Interfaces
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> SearchAsync(
            string? name,
            Guid? categoryId);
    }
}
