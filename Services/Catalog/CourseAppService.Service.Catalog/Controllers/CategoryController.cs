using CourseApp.Shared.Extensions;
using CourseAppService.Service.Catalog.Models;
using CourseAppService.Service.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppService.Service.Catalog.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(data);
    }
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery]string id)
    {
        var data =await _categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(data);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Category category)
    {
        var data = await _categoryService.CreateAsync(category);
        return CreateActionResultInstance(data);
    }

}