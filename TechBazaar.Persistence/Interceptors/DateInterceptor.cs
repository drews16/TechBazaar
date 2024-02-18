using Microsoft.EntityFrameworkCore.Diagnostics;
using TechBazaar.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TechBazaar.Persistence.Interceptors
{
    public sealed class DateInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if(dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker
                .Entries<IAuditable>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                .ToList();
                
            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
                
                if(entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                 }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}