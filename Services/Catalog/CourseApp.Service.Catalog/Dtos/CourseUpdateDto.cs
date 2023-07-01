namespace CourseApp.Service.Catalog.Dtos;

public sealed record CourseUpdateDto
{
    public string? Id { get; init; }
    public string? UserId { get; init; }
    public string? Picture { get; set; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    
    public decimal Price { get; init; }

    public FeatureDto FeatureDto { get; init; } 
    
    public string? CategoryId { get; init; }
}