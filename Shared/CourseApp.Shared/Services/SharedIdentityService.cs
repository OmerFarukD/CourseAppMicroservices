using Microsoft.AspNetCore.Http;

namespace CourseApp.Shared.Services;

public class SharedIdentityService : ISharedIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId => _httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(x => x.Type.Equals("sub")).Value;
}