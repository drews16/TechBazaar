using TechBazaar.Domain.Dto.Category;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Extensions
{
    public static class CategoryExtension
    {
        public static CategoryDto ToDto(this Category entity)
        {
            return new CategoryDto
            (
                entity.Id,
                entity.Name,
                entity.DisplayName,
                entity.UrlName
            );
        }

        public static Category ToEntity(this CategoryDto dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                UrlName = dto.UrlName
            };
        }
    }
}
