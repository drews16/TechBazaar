using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        Task<Order> InsertAsync(Order order);
        Task<Order> UpdateAsync(Order order);
    }
}