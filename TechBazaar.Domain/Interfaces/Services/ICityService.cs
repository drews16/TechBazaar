using TechBazaar.Domain.Dto.City;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ICityService
    {
        Task<BaseResult<IEnumerable<CityDto>>> GetAllCitiesAsync();
    }
}