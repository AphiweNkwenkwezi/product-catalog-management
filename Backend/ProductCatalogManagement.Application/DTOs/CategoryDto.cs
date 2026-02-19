namespace ProductCatalogManagement.Application.DTOs
{
    public record CategoryDto(
        Guid Id,
        string Name,
        string? Description,
        Guid? ParentId,
        string? ParentName
    );
}
