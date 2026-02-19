using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task SaveChangesAsync();
        Task<List<Guid>> GetDescendantIdsAsync(Guid categoryId);
    }
}
