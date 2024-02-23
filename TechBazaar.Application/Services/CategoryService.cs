using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.Category;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class CategoryService(
        IBaseRepository<Category> categoryRepository,
        ILogger logger) : ICategoryService
    {
        public async Task<BaseResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            CategoryDto[] categories;

            try
            {
                categories = await categoryRepository
                    .GetAll()
                    .Select(x => new CategoryDto
                    (
                        x.Id,
                        x.Name,
                        x.DisplayName,
                        x.UrlName
                    ))
                    .ToArrayAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<IEnumerable<CategoryDto>>
                {
                    ErrorMessage = "Произошла внутренняя ошибка сервера"
                };
            }

            if(!categories.Any())
            {
                return new BaseResult<IEnumerable<CategoryDto>>
                {
                    ErrorMessage = "Список категорий пуст"
                };
            }

            return new BaseResult<IEnumerable<CategoryDto>>
            {
                Data = categories
            };
        }
    }
}