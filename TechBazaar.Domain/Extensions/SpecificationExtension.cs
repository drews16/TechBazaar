using TechBazaar.Domain.Dto.Specification;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Extensions
{
    public static class SpecificationExtension
    {
        public static SpecificationDto ToDto(this Specification entity)
        {
            return new SpecificationDto(
                entity.SpecificationType.SpecificationName, 
                entity.SpecificationType.EngUnit,
                entity.Value);
        }
    }
}