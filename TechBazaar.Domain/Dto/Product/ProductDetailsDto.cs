using TechBazaar.Domain.Dto.ProductImage;
using TechBazaar.Domain.Dto.Specification;

namespace TechBazaar.Domain.Dto.Product
{
    public sealed record ProductDetailsDto(long Id, string BrandName, string Model, string CategoryName, decimal Price,
        int AvailableQuantity, IEnumerable<SpecificationDto> Specifications, IEnumerable<ProductImageDto> ProductImages);
}
