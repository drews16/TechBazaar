using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Interfaces.Repositories
{
    public interface ICityRepository
    {
        IQueryable<City> GetAll();
    }
}