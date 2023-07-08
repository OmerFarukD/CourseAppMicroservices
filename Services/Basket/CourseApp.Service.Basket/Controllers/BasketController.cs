using CourseApp.Service.Basket.Dto;
using CourseApp.Service.Basket.Services;
using CourseApp.Shared.Extensions;
using CourseApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Service.Basket.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BasketController : BaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }
    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var claims = User.Claims;
        
        return CreateActionResultInstance(await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdate([FromBody] BasketDto basketDto)
    {
        return CreateActionResultInstance(await _basketService.SaveOrUpdate(basketDto));
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromQuery]string userId)
    {
        return CreateActionResultInstance(await _basketService.Delete(userId));
    }
}