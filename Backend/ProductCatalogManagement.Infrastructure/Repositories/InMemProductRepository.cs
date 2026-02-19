using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Infrastructure.Repositories
{
    public class InMemProductRepository : IProductRepository
    {
        private readonly List<ProductEntity> _products = new();

        public Task<IEnumerable<ProductEntity>> GetAllAsync()
            => Task.FromResult(_products.AsEnumerable());

        public Task<ProductEntity?> GetByIdAsync(Guid id)
            => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task AddAsync(ProductEntity entity)
        {
            _products.Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ProductEntity entity)
        {
            var index = _products.FindIndex(p => p.Id == entity.Id);
            if (index >= 0)
            {
                _products[index] = entity;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _products.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProductEntity>> SearchAsync(string? name, Guid? categoryId)
        {
            var query = _products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p =>
                    p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            return Task.FromResult(query);
        }
    }
}
