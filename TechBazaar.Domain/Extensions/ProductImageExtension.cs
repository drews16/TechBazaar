using TechBazaar.Domain.Dto.ProductImage;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Extensions
{
    public static class ProductImageExtension
    {
        public static ProductImageDto ToDto(this ProductImage entity)
        {
            return new ProductImageDto(entity.Id, entity.Path);
        }
    }
}