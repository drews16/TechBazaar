using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.CartProduct;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class CartProductService(
        IBaseRepository<CartProduct> cartProductRepository,
        ILogger logger) : ICartProductService
    {
        public async Task<BaseResult<long>> AddProductInCart(long cartId, CreateCartProductDto dto)
        {
            CartProduct? cartProduct;

            try
            {
                cartProduct = await cartProductRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.ProductId == dto.ProductId);

                if(cartProduct != null)
                {
                    cartProduct.Count += dto.Count;

                    await cartProductRepository.UpdateAsync(cartProduct);

                    return new BaseResult<long>()
                    {
                        Data = cartProduct.Id
                    };
                }

                cartProduct = new CartProduct
                {
                    CartId = cartId,
                    ProductId = dto.ProductId,
                    Count = dto.Count
                };

                await cartProductRepository.InsertAsync(cartProduct);

                return new BaseResult<long>
                {
                    Data = cartProduct.Id
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<long>
                {
                    ErrorMessage = "Произошла внутренняя ошибка сервера"
                };
            }
        }

        public async Task<BaseResult<IEnumerable<CartProductDto>>> GetCartProducts(long cartId)
        {
            CartProductDto[] cartProducts;

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
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<IEnumerable<CartProductDto>>
                {
                    ErrorMessage = "Произошла внутрення ошибка сервера"
                };
            }

            if(!cartProducts.Any())
            {
                return new BaseResult<IEnumerable<CartProductDto>>
                {
                    ErrorMessage = "Товары в корзине не найдены"
                };
            }

            return new BaseResult<IEnumerable<CartProductDto>>
            {
                Data = cartProducts
            };
        }
    }
}