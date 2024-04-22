using TechBazaar.Domain.Dto.CartProduct;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ICartProductService
    {
        /// <summary>
        /// Добавление товара в корзину (обновление, если уже в корзине)
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<long>> AddCartProductAsync(long cartId, CreateCartProductDto dto);
        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        Task<BaseResult<long>> RemoveCartProductAsync(long cartProductId);
    }
}