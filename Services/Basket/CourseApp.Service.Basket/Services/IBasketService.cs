using CourseApp.Service.Basket.Dto;
using CourseApp.Shared.Dtos;

namespace CourseApp.Service.Basket.Services;

public interface IBasketService
{
    Task<Response<BasketDto>> GetBasketAsync(string userId);
    Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
    Task<Response<bool>> Delete(string userId);
}