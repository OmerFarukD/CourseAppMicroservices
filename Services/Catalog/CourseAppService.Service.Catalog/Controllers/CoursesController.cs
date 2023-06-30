using CourseApp.Shared.Extensions;
using CourseAppService.Service.Catalog.Dtos;
using CourseAppService.Service.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseAppService.Service.Catalog.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery]string id)
    {
        var response = await _courseService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _courseService.GetAllAsync();
        return CreateActionResultInstance(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUserId([FromQuery] string userId)
    {
        var data = await _courseService.GetAllByUserIdAsync(userId);
        return CreateActionResultInstance(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CourseCreateDto courseCreateDto)
    {
        var response = await _courseService.CreateAsync(courseCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] CourseUpdateDto courseUpdateDto)
    {
        var response = await _courseService.UpdateAsync(courseUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _courseService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}