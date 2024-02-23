using TechBazaar.Domain.Dto.Category;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<CategoryDto>>> GetCategories();
    }
}