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
        public async Task<BaseResult<long>> AddCartProductAsync(long cartId, CreateCartProductDto dto)
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

        public async Task<BaseResult<long>> RemoveCartProductAsync(long cartProductId)
        {
            try
            {
                var cartProduct = await cartProductRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Id == cartProductId);

                if(cartProduct == null)
                {
                    return new BaseResult<long>
                    {
                        ErrorMessage = "Товар не найден"
                    };
                }

                await cartProductRepository.RemoveAsync(cartProduct);

                return new BaseResult<long>
                { 
                    Data = cartProduct.Id
                };
            }
            catch (Exception ex)
            {
                return new BaseResult<long>
                {
                    ErrorMessage = "Произошла внутренняя ошибка сервера"
                };
            }
        }
    }
}