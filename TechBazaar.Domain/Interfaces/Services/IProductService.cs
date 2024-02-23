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
        Task<BaseResult<IEnumerable<ProductCatalogDto>>> GetProductsAsync(string category, string orderByPrice, string  orderByPopularity);
        /// <summary>
        /// Получение детальной информации о продукте
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<BaseResult<ProductDetailsDto>> GetProductDetailsAsync(long productId);
        /// <summary>
        /// Получение самых продоваемых товаров
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<ProductCatalogDto>>> GetBestSellingProducts(int count);
    }
}