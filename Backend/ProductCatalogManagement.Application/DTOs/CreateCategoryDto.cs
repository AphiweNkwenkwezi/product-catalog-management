namespace ProductCatalogManagement.Application.DTOs
{
    public record CreateCategoryDto(
        string Name,
        string? Description,
        Guid? ParentId
    );
}
