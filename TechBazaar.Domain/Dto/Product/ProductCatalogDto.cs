namespace TechBazaar.Domain.Dto.Product
{
    public sealed record ProductCatalogDto(long Id, string BrandName, string Model, string CategoryName, decimal Price,
        string MainImage, int CountPurchase, int AvailableQuantity);
}