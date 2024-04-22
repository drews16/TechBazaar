using Microsoft.EntityFrameworkCore;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;

namespace TechBazaar.Persistence.Repositories
{
    public sealed class OfficeRepository(
        ApplicationDbContext context) : IOfficeRepository
    {
        public async Task<IEnumerable<Office>> GetByCityId(int cityId)
        {
            return await context.Offices
                .AsNoTracking()
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }
    }
}