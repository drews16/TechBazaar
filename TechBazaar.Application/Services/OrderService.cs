using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.Order;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Enum;
using TechBazaar.Domain.Extensions;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class OrderService (
        IOrderRepository orderRepository,
        IBaseRepository<CartProduct> cartProductRepository,
        ILogger logger) : IOrderService
    {
        public async Task<BaseResult> CreateOrderAsync(CreateOrderDto dto)
        {
            var products = await cartProductRepository
                .GetAll()
                .Where(x => x.CartId == dto.CartId)
                .ToListAsync();

            var order = new Order
            {
                UserId = dto.UserId,
                OfficeId = dto.OfficeId,
                Status = OrderStatusEnum.Payed.GetDisplayName(),
                TotalPrice = dto.TotalPrice,
                OrderProducts = products
                    .Select(x => new OrderProduct {
                        ProductId = x.ProductId,
                        Count = x.Count
                    })
                    .ToList()
            };

            await orderRepository.InsertAsync(order);

            return new BaseResult();
        }

        public async Task<BaseResult<OrderInfoDto>> GetOrderInfo(long cartId)
        {
            var cartProducts = await cartProductRepository
                .GetAll()
                .Where(x => x.CartId == cartId)
                .Include(x => x.Product)
                .ToListAsync();

            return new BaseResult<OrderInfoDto>
            {
                Data = new OrderInfoDto(
                    ProductCount: cartProducts.Sum(x => x.Count),
                    TotalPrice: cartProducts.Sum(x => x.Count * x.Product.Price)
                )
            };
        }

        public async Task<BaseResult<IEnumerable<OrderDto>>> GetUserOrdersAsync(Guid userId)
        {
            OrderDto[] userOrders = [];

            try
            {
                userOrders = await orderRepository
                    .GetAll()
                    .Include(x => x.Office)
                    .Include(x => x.OrderProducts)
                    .Where(x => x.UserId == userId)
                    .Select(x => new OrderDto
                    (
                        x.Id,
                        x.Office.City.Name,
                        x.Office.Address,
                        x.Status,
                        x.TotalPrice,
                        x.OrderProducts.Count()
                    ))
                    .ToArrayAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<IEnumerable<OrderDto>>
                {
                    ErrorMessage = "Произошла внутренняя ошибка сервера"
                };
            }

            if(!userOrders.Any())
            {
                return new BaseResult<IEnumerable<OrderDto>>
                {
                    ErrorMessage = "Список заказов пуст"
                };
            }

            return new BaseResult<IEnumerable<OrderDto>>
            {
                Data = userOrders
            };
        }
    }
}