using TechBazaar.Domain.Dto.OrderProduct;

namespace TechBazaar.Domain.Dto.Order
{
    public sealed record OrderDto(
        long Id,
        string City,
        string OfficeAddress,
        string Status, 
        decimal TotalPrice, 
        int ProductsCount);
}