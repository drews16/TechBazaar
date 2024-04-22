using TechBazaar.Domain.Dto.Office;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface IOfficeService
    {
        Task<BaseResult<IEnumerable<OfficeDto>>> GetByCityId(int cityId);
    }
}