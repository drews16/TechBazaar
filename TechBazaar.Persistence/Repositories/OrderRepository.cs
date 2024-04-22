using Microsoft.EntityFrameworkCore;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;

namespace TechBazaar.Persistence.Repositories
{
    public sealed class OrderRepository(
        ApplicationDbContext context) : IOrderRepository
    {
        public IQueryable<Order> GetAll()
        {
            return context.Orders
                .AsNoTracking();
        }

        public async Task<Order> InsertAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("Entity is null");

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("Entity is null");

            context.Orders.Update(order);
            await context.SaveChangesAsync();

            return order;
        }
    }
}