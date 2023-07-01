namespace CourseApp.Service.Catalog.Dtos;

public sealed record CategoryDto
{
    public string? Id { get; init; }
    public string? Name { get; init; }
}