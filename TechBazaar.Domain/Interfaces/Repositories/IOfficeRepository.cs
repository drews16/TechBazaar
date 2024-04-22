using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<IEnumerable<Office>> GetByCityId(int cityId);
    }
}