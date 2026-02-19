using ProductCatalogManagement.Application.DTOs;

namespace ProductCatalogManagement.Application.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        //Task<IEnumerable<CategoryTreeDto>> GetCategoryTreeAsync();
        Task<CategoryDto?> GetByIdAsync(Guid id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Guid>> GetDescendantIdsAsync(Guid id);
    }
}
