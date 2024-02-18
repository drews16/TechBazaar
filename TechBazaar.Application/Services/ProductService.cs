using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.Product;
using TechBazaar.Domain.Dto.ProductImage;
using TechBazaar.Domain.Dto.Specification;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class ProductService (
        IBaseRepository<Product> productRepository,
        ILogger logger) : IProductService
    {
        public async Task<BaseResult<ProductDetailsDto>> GetProductDetailsAsync(long productId)
        {
            ProductDetailsDto? product;

            try
            {
                var products = await productRepository
                    .GetAll()
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Include(x => x.Specifications)
                        .ThenInclude(x => x.SpecificationType)
                    .Include(x => x.ProductImages)
                    .Select(x => new ProductDetailsDto
                    (
                        x.Id,
                        x.Brand.Name,
                        x.Model,
                        x.Category.Name,
                        x.Price,
                        x.AvailableQuantity,
                        x.Specifications.Select(x => new SpecificationDto
                        (
                            x.SpecificationType.DisplayName,
                            x.SpecificationType.EngUnit,
                            x.Value
                        )),
                        x.ProductImages.Select(x => new ProductImageDto(x.Id, x.Path))
                    )).ToListAsync();

                product = products
                    .FirstOrDefault(x => x.Id == productId);
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<ProductDetailsDto>
                {
                    ErrorMessage = "Произошла внутреняя ошибка сервера"
                };
            }

            if (product == null)
            {
                return new BaseResult<ProductDetailsDto>
                {
                    ErrorMessage = "Товар не найден"
                };
            }

            return new BaseResult<ProductDetailsDto>
            {
                Data = product
            };
        }

        public async Task<BaseResult<IEnumerable<ProductCatalogDto>>> GetProductsAsync(string category)
        {
            ProductCatalogDto[] products;

            try
            {
                products = await productRepository
                    .GetAll()
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Where(x => x.Category.UrlName == category)
                    .Select(x => new ProductCatalogDto
                    (
                        x.Id,
                        x.Brand.Name,
                        x.Model,
                        x.Category.Name,
                        x.Price,
                        x.MainImage,
                        x.CountPurchase,
                        x.AvailableQuantity
                    ))
                    .ToArrayAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<IEnumerable<ProductCatalogDto>>
                {
                    ErrorMessage = "Произошла внутрення ошибка сервера"
                };
            }

            if(!products.Any())
            {
                return new BaseResult<IEnumerable<ProductCatalogDto>>
                {
                    ErrorMessage = "Товары не найдены"
                };
            }

            return new BaseResult<IEnumerable<ProductCatalogDto>>
            {
                Data = products
            };
        }
    }
}