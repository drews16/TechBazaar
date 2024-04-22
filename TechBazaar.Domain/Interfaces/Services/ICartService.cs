using TechBazaar.Domain.Dto.Cart;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Получение корзины с товарами
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<BaseResult<CartDto>> GetCartAsync(long cartId);
    }
}