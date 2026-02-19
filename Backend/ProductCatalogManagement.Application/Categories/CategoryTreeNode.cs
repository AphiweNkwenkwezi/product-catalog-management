namespace ProductCatalogManagement.Application.Categories
{
    public class CategoryTreeNode
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public List<CategoryTreeNode> Children { get; init; } = new();
    }
}
