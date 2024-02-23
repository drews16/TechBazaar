using Microsoft.EntityFrameworkCore;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;

namespace TechBazaar.Persistence.Repositories
{
    public sealed class BaseRepository<TEntity>
        (ApplicationDbContext context) : IBaseRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetAll()
        {
            return context
                .Set<TEntity>()
                .AsNoTracking();
         
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Entity is null");
            }

            await context.AddAsync(entity);
            await context.SaveChangesAsync();  

            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null");
            }

            context.Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is null");
            }

            context.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}