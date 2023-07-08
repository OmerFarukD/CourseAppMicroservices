using System.Diagnostics;
using System.Text.Json;
using CourseApp.Service.Basket.Dto;
using CourseApp.Shared.Dtos;

namespace CourseApp.Service.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<BasketDto>> GetBasketAsync(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);
        if (string.IsNullOrWhiteSpace(existBasket))
        {
            return Response<BasketDto>.Fail("Basket Not Found",400);
        }
        
        return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),200);
    }

    public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket Could Not Update Or Save", 500);
    }

    public async Task<Response<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket Could Not Found", 400);
    }
    
}