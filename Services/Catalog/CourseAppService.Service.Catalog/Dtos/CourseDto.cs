namespace CourseAppService.Service.Catalog.Dtos;

public sealed record CourseDto
{

    public string? Id { get; init; }

    public string? UserId { get; init; }
    
    public string? Name { get; init; }
    public string? Description { get; init; }


    public decimal Price { get; init; }

    public string? Picture { get; init; }


    public DateTime CreatedTime { get; init; }

    public FeatureDto FeatureDto { get; init; } 
    
    public string? CategoryId { get; init; }


    public CategoryDto CategoryDto { get; init; }
}