using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.City;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class CityService(
        ICityRepository cityRepository,
        ILogger logger) : ICityService
    {
        public async Task<BaseResult<IEnumerable<CityDto>>> GetAllCitiesAsync()
        {
            CityDto[] cities;

            try
            {
                cities = await cityRepository
                    .GetAll()
                    .Select(x => new CityDto(
                        x.Id,
                        x.Name))
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<IEnumerable<CityDto>>
                {
                    ErrorMessage = "Произошла ошибка сервера"
                };
            }

            if(!cities.Any())
            {
                return new BaseResult<IEnumerable<CityDto>>
                {
                    ErrorMessage = "Список городов пуст"
                };
            }

            return new BaseResult<IEnumerable<CityDto>>
            {
                Data = cities
            };
        }
    }
}