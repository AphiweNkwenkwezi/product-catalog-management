using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.Application.Categories
{
    public static class CategoryTreeBuilder
    {
        public static List<CategoryTreeNode> Build(IEnumerable<CategoryEntity> categories)
        {
            var lookup = categories.ToLookup(c => c.ParentCategoryId);

            List<CategoryTreeNode> BuildNodes(Guid? parentId)
            {
                return lookup[parentId]
                    .Select(category => new CategoryTreeNode
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Children = BuildNodes(category.Id)
                    })
                    .ToList();
            }

            return BuildNodes(null);
        }
    }
}
