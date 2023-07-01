using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Models;
using CourseApp.Shared.Dtos;

namespace CourseApp.Service.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(Category category);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    
}