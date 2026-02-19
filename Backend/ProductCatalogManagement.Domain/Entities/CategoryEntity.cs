namespace ProductCatalogManagement.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Guid? ParentCategoryId { get; private set; }

        public CategoryEntity? Parent { get; private set; }

        private CategoryEntity() { } // EF

        public CategoryEntity(Guid id, string name, string? description, Guid? parentCategoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public void Update(
            string name,
            string? description,
            Guid? parentCategoryId)
        {
            SetName(name);
            SetDescription(description);
            SetParent(parentCategoryId);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Name = name.Trim();
        }

        private void SetDescription(string? description)
        {
            Description = string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
        }

        private void SetParent(Guid? parentCategoryId)
        {
            if (parentCategoryId == Id)
                throw new InvalidOperationException("Category cannot be its own parent.");

            ParentCategoryId = parentCategoryId;
        }
    }
}
