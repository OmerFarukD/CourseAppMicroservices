namespace CourseApp.Service.Basket.Dto;

public class BasketDto
{
    public List<BasketItemDto> BasketItems { get; set; }
    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    public decimal TotalPrice
    {
        get => BasketItems.Sum(x => x.Quantity * x.Price);
    }
}