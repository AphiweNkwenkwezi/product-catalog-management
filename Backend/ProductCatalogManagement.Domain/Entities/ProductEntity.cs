namespace ProductCatalogManagement.Domain.Entities
{
    public class ProductEntity : IComparable<ProductEntity>
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public string SKU { get; private set; } = string.Empty;

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public Guid? CategoryId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        private ProductEntity() { }

        public ProductEntity(
            Guid id,
            string name,
            string sku,
            decimal price,
            int quantity,
            Guid? categoryId,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU is required.", nameof(sku));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));

            Id = id;
            Name = name;
            SKU = sku;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            Description = description;

            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateStock(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(newQuantity));

            Quantity = newQuantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Increase amount must be positive.", nameof(amount));

            Quantity += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Decrease amount must be positive.", nameof(amount));

            if (Quantity - amount < 0)
                throw new InvalidOperationException("Insufficient stock.");

            Quantity -= amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            string name,
            string sku,
            decimal price,
            Guid? categoryId,
            string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU is required.", nameof(sku));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            Name = name;
            SKU = sku;
            Price = price;
            CategoryId = categoryId;
            Description = description;

            UpdatedAt = DateTime.UtcNow;
        }

        public int CompareTo(ProductEntity? other)
        {
            if (other is null) return 1;

            return Price.CompareTo(other.Price);
        }
    }
}
