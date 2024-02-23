using TechBazaar.Domain.Dto.Product;

namespace TechBazaar.Domain.Dto.CartProduct
{
    public sealed record CartProductDto(long Id, string BrandName, string Model, string CategoryName, decimal Price,
        string MainImage, int AvailableQuantity, int Count);
}