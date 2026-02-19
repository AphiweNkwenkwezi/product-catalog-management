namespace ProductCatalogManagement.Application.DTOs
{
    public record UpdateProductDto(
        string Name,
        string? Description,
        string SKU,
        decimal Price,
        int Quantity,
        Guid? CategoryId
    );
}
