using ProductCatalogManagement.Application.DTOs;
using ProductCatalogManagement.Application.Models;

namespace ProductCatalogManagement.Application.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllAsync(
            string? search,
            Guid? categoryId,
            int page,
            int pageSize);

        Task<ProductDto?> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(CreateProductDto dto);

        Task UpdateAsync(Guid id, UpdateProductDto dto);

        Task DeleteAsync(Guid id);
    }
}
