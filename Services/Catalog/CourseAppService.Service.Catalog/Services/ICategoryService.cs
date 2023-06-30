using CourseApp.Shared.Dtos;
using CourseAppService.Service.Catalog.Dtos;
using CourseAppService.Service.Catalog.Models;

namespace CourseAppService.Service.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(Category category);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    
}