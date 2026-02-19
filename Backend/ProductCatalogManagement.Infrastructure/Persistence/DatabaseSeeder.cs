using ProductCatalogManagement.DataContext;
using ProductCatalogManagement.Domain.Entities;

namespace ProductCatalogManagement.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Products.Any())
            return;

        var electronics = new CategoryEntity(Guid.NewGuid(), "Electronics", "Electronic devices", null);
        var accessories = new CategoryEntity(Guid.NewGuid(), "Accessories", "Tech accessories", null);
        var appliances = new CategoryEntity(Guid.NewGuid(), "Home Appliances", "Home & kitchen appliances", null);

        var laptops = new CategoryEntity(Guid.NewGuid(), "Laptops", "Portable computers", electronics.Id);
        var smartphones = new CategoryEntity(Guid.NewGuid(), "Smartphones", "Mobile devices", electronics.Id);
        var monitors = new CategoryEntity(Guid.NewGuid(), "Monitors", "Display screens", electronics.Id);

        var keyboards = new CategoryEntity(Guid.NewGuid(), "Keyboards", "Computer keyboards", accessories.Id);
        var mice = new CategoryEntity(Guid.NewGuid(), "Mice", "Computer mice", accessories.Id);

        var kitchen = new CategoryEntity(Guid.NewGuid(), "Kitchen", "Kitchen appliances", appliances.Id);

        context.Categories.AddRange(
            electronics, accessories, appliances,
            laptops, smartphones, monitors,
            keyboards, mice, kitchen
        );

        context.Products.AddRange(

            // Laptops
            new ProductEntity(Guid.NewGuid(), "Laptop Pro 15", "LTP-001", 15000, 10, laptops.Id, "High performance laptop"),
            new ProductEntity(Guid.NewGuid(), "Laptop Air 13", "LTP-002", 12000, 5, laptops.Id, "Lightweight laptop"),
            new ProductEntity(Guid.NewGuid(), "Gaming Beast X", "LTP-003", 22000, 3, laptops.Id, "High-end gaming laptop"),

            // Smartphones
            new ProductEntity(Guid.NewGuid(), "Galaxy Ultra 24", "PHN-001", 18000, 8, smartphones.Id, "Flagship smartphone"),
            new ProductEntity(Guid.NewGuid(), "iFruit Pro Max", "PHN-002", 21000, 6, smartphones.Id, "Premium smartphone"),
            new ProductEntity(Guid.NewGuid(), "Budget Smart Lite", "PHN-003", 4500, 15, smartphones.Id, "Affordable smartphone"),

            // Monitors
            new ProductEntity(Guid.NewGuid(), "UltraWide 34\"", "MON-001", 9000, 4, monitors.Id, "34-inch ultrawide monitor"),
            new ProductEntity(Guid.NewGuid(), "4K Pro Display", "MON-002", 12000, 2, monitors.Id, "4K professional monitor"),

            // Keyboards
            new ProductEntity(Guid.NewGuid(), "Mechanical RGB Keyboard", "KEY-001", 1800, 20, keyboards.Id, "Mechanical gaming keyboard"),
            new ProductEntity(Guid.NewGuid(), "Wireless Slim Keyboard", "KEY-002", 950, 12, keyboards.Id, "Minimal wireless keyboard"),

            // Mice
            new ProductEntity(Guid.NewGuid(), "Ergo Mouse Pro", "MSE-001", 1200, 9, mice.Id, "Ergonomic office mouse"),
            new ProductEntity(Guid.NewGuid(), "Gaming Precision X", "MSE-002", 1500, 7, mice.Id, "High DPI gaming mouse"),

            // Kitchen
            new ProductEntity(Guid.NewGuid(), "Air Fryer XL", "KIT-001", 3200, 11, kitchen.Id, "Large capacity air fryer"),
            new ProductEntity(Guid.NewGuid(), "Smart Kettle", "KIT-002", 1500, 14, kitchen.Id, "WiFi enabled kettle")
        );

        context.SaveChanges();
    }
}
