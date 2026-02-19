namespace ProductCatalogManagement.Application.DTOs
{
    public record UpdateCategoryDto(
        string Name,
        string? Description,
        Guid? ParentId
    );
}
