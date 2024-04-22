using Microsoft.IdentityModel.Abstractions;
using TechBazaar.Domain.Dto.Office;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class OfficeService(
        IOfficeRepository officeRepository) : IOfficeService
    {
        public async Task<BaseResult<IEnumerable<OfficeDto>>> GetByCityId(int cityId)
        {
            var offices = await officeRepository
                .GetByCityId(cityId);

            return new BaseResult<IEnumerable<OfficeDto>>
            {
                Data = offices
                    .Select(x => new OfficeDto(
                        Id: x.Id,
                        CityId: x.CityId,
                        Address: x.Address)
                    )
            };
        }
    }
}
