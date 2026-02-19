using ProductCatalogManagement.Application.DTOs;
using ProductCatalogManagement.Application.Models;
using ProductCatalogManagement.Application.Search;
using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ProductSearchEngine<ProductEntity> _searchEngine;

        public ProductService(
            IProductRepository productRepo,
            ICategoryRepository categoryRepo,
            ProductSearchEngine<ProductEntity> searchEngine)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _searchEngine = searchEngine;
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(
            string? search,
            Guid? categoryId,
            int page,
            int pageSize)
        {
            var products = await _productRepo.GetAllAsync();

            // Category filtering
            if (categoryId.HasValue)
            {
                var categoryIds = await _categoryRepo
                    .GetDescendantIdsAsync(categoryId.Value);

                products = products.FilterByCategory(categoryIds);
            }

            // Search filtering
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = _searchEngine.Search(
                    products,
                    search,
                    p => p.Name,
                    p => p.Description ?? string.Empty
                );
            }

            var totalCount = products.Count();

            var pagedItems = products
                .Paginate(page, pageSize)
                .Select(MapToDto)
                .ToList();

            return new PagedResult<ProductDto>(
                pagedItems,
                totalCount,
                page,
                pageSize);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return product is null ? null : MapToDto(product);
        }

        public async Task<Guid> CreateAsync(CreateProductDto dto)
        {
            ValidateCreate(dto);

            var product = new ProductEntity(
                Guid.NewGuid(),
                dto.Name,
                dto.SKU,
                dto.Price,
                dto.Quantity,
                dto.CategoryId,
                dto.Description
            );

            await _productRepo.AddAsync(product);

            return product.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _productRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Product not found.");

            product.UpdateDetails(
                dto.Name,
                dto.SKU,
                dto.Price,
                dto.CategoryId,
                dto.Description
            );

            product.UpdateStock(dto.Quantity);

            await _productRepo.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepo.DeleteAsync(id);
        }

        private static ProductDto MapToDto(ProductEntity product)
            => new(
                product.Id,
                product.Name,
                product.Description,
                product.SKU,
                product.Price,
                product.Quantity,
                product.CategoryId
            );

        private static void ValidateCreate(CreateProductDto dto)
        {
            _ = dto switch
            {
                { Name: null or "" } =>
                    throw new ArgumentException("Name is required."),
                { SKU: null or "" } =>
                    throw new ArgumentException("SKU is required."),
                { Price: <= 0 } =>
                    throw new ArgumentException("Price must be greater than zero."),
                { Quantity: < 0 } =>
                    throw new ArgumentException("Quantity cannot be negative."),
                _ => dto
            };
        }
    }
}
