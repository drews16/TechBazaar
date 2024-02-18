using TechBazaar.Domain.Dto.Product;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Получение продуктов по категории
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<ProductCatalogDto>>> GetProductsAsync(string category);
        /// <summary>
        /// Получение детальной информации о продукте
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<BaseResult<ProductDetailsDto>> GetProductDetailsAsync(long productId);
    }
}