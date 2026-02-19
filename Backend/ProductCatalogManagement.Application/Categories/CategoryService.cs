using ProductCatalogManagement.Application.DTOs;
using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = (await _repo.GetAllAsync()).ToList();

            return categories.Select(c => MapToDto(c, categories));
        }

        public async Task<List<CategoryTreeNode>> GetCategoryTreeAsync()
        {
            var categories = await _repo.GetAllAsync();

            return CategoryTreeBuilder.Build(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id)
        {
            var categories = (await _repo.GetAllAsync()).ToList();

            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                return null;

            return MapToDto(category, categories);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            if (dto.ParentId.HasValue)
            {
                var parent = await _repo.GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                    throw new InvalidOperationException("Parent category does not exist.");
            }

            var entity = new CategoryEntity(
                Guid.NewGuid(),
                dto.Name,
                dto.Description,
                dto.ParentId
            );

            await _repo.AddAsync(entity);

            var categories = (await _repo.GetAllAsync()).ToList();

            return MapToDto(entity, categories);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                return false;

            if (dto.ParentId == id)
                throw new InvalidOperationException("Category cannot be its own parent.");

            if (dto.ParentId.HasValue)
            {
                var descendantIds = await _repo.GetDescendantIdsAsync(id);

                if (descendantIds.Contains(dto.ParentId.Value))
                    throw new InvalidOperationException("Cannot assign a descendant as parent.");
            }

            category.Update(
                dto.Name,
                dto.Description,
                dto.ParentId
            );

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                return false;

            var allCategories = await _repo.GetAllAsync();

            if (allCategories.Any(c => c.ParentCategoryId == id))
                throw new InvalidOperationException("Cannot delete category with children.");

            await _repo.DeleteAsync(id);

            return true;
        }

        public async Task<List<Guid>> GetDescendantIdsAsync(Guid id)
        {
            var categories = await _repo.GetAllAsync();

            var all = categories.ToList();
            var result = new List<Guid>();

            void AddChildren(Guid parentId)
            {
                var children = all
                    .Where(c => c.ParentCategoryId == parentId)
                    .ToList();

                foreach (var child in children)
                {
                    result.Add(child.Id);
                    AddChildren(child.Id);
                }
            }

            AddChildren(id);

            return result;
        }

        private static CategoryDto MapToDto(
            CategoryEntity category,
            IEnumerable<CategoryEntity> allCategories)
        {
            var parent = allCategories
                .FirstOrDefault(c => c.Id == category.ParentCategoryId);

            return new CategoryDto(
                category.Id,
                category.Name,
                category.Description,
                category.ParentCategoryId,
                parent?.Name
            );
        }
    }
}
