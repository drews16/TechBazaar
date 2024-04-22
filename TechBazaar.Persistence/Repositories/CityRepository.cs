using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;

namespace TechBazaar.Persistence.Repositories
{
    public sealed class CityRepository(
        ApplicationDbContext context) : ICityRepository
    {
        public IQueryable<City> GetAll()
        {
            return context.Cities;
        }
    }
}