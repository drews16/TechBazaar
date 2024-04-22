namespace TechBazaar.Domain.Dto.OrderProduct
{
    public sealed record OrderProductDto (long Id, long ProductId, long OrderId, int Count);
}