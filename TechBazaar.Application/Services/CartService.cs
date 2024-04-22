using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.Cart;
using TechBazaar.Domain.Dto.CartProduct;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class CartService(
        IBaseRepository<CartProduct> cartProductRepository,
        ILogger logger) : ICartService
    {
        public async Task<BaseResult<CartDto>> GetCartAsync(long cartId)
        {
            CartProductDto[] cartProducts;
            decimal cartTotalPrice = 0;

            try
            {
                cartProducts = await cartProductRepository
                    .GetAll()
                    .Where(x => x.CartId == cartId)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Category)
                    .Select(x => new CartProductDto
                    (
                        x.Id,
                        x.Product.Id,
                        x.Product.Brand.Name,
                        x.Product.Model,
                        x.Product.Category.Name,
                        x.Product.Price,
                        x.Product.MainImage,
                        x.Product.AvailableQuantity,
                        x.Count
                    ))
                    .ToArrayAsync(); cartProducts = await cartProductRepository
                    .GetAll()
                    .Where(x => x.CartId == cartId)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Category)
                    .Select(x => new CartProductDto
                    (
                        x.Id,
                        x.Product.Id,
                        x.Product.Brand.Name,
                        x.Product.Model,
                        x.Product.Category.Name,
                        x.Product.Price,
                        x.Product.MainImage,
                        x.Product.AvailableQuantity,
                        x.Count
                    ))
                    .ToArrayAsync();

                cartTotalPrice = cartProducts
                    .Sum(x => x.Price * x.Count);
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<CartDto>
                {
                    ErrorMessage = "Произошла внутренняя ошибка сервера"
                };
            }

            if (!cartProducts.Any())
            {
                return new BaseResult<CartDto>
                {
                    ErrorMessage = "Товары в корзине не найдены"
                };
            }

            return new BaseResult<CartDto>
            {
                Data = new CartDto
                (
                    cartProducts,
                    cartTotalPrice
                )
            };
        }
    }
}
