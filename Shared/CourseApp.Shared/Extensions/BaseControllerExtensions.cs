using CourseApp.Shared.Dtos;

namespace CourseApp.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
public class BaseController : ControllerBase
{
    public static IActionResult CreateActionResultInstance<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };

    }
}