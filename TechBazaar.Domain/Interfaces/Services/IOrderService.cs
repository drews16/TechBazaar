using TechBazaar.Domain.Dto.Order;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Получение закозов пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<BaseResult<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId);
        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult> CreateOrderAsync(CreateOrderDto dto);
        Task<BaseResult<OrderInfoDto>> GetOrderInfo(long cartId);
    }
}