namespace CourseAppService.Service.Catalog.Dtos;

public record CourseCreateDto
{
    public string? UserId { get; init; }
    
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Picture { get; set; }
    public decimal Price { get; init; }

    public FeatureDto FeatureDto { get; init; } 
    
    public string? CategoryId { get; init; }
    
}