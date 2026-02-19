using Microsoft.EntityFrameworkCore;
using ProductCatalogManagement.DataContext;
using ProductCatalogManagement.Domain.Entities;
using ProductCatalogManagement.Domain.Interfaces;

namespace ProductCatalogManagement.Infrastructure.Repositories
{
    public class CategoryRepository
        : Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<Guid>> GetDescendantIdsAsync(Guid categoryId)
        {
            var allCategories = await _context.Categories.ToListAsync();
            var result = new List<Guid>();

            void AddChildren(Guid parentId)
            {
                var children = allCategories
                    .Where(c => c.ParentCategoryId == parentId)
                    .ToList();

                foreach (var child in children)
                {
                    result.Add(child.Id);
                    AddChildren(child.Id);
                }
            }

            result.Add(categoryId);
            AddChildren(categoryId);

            return result;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
