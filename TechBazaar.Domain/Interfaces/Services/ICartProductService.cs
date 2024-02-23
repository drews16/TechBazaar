using TechBazaar.Domain.Dto.CartProduct;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ICartProductService
    {
        /// <summary>
        /// Получение всех товаров в корзине
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<CartProductDto>>> GetCartProducts(long cartId);
        /// <summary>
        /// Добавление товара в корзину (обновление, если уже в корзине)
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<long>> AddProductInCart(long cartId, CreateCartProductDto dto);
    }
}