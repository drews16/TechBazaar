namespace TechBazaar.Domain.Dto.Order
{
    public sealed record OrderInfoDto(
        int ProductCount,
        decimal TotalPrice
    );
}