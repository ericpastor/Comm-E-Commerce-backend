using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Comm.Core.src.Entities;

namespace Comm.WebAPI.src.Database
{
    public class TimeStampAsyncInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var changedData = eventData.Context.ChangeTracker.Entries(); // colección de todas las entidades que experimentan cambios: Agregadas o Actualizadas, Eliminadas
            var updatedEntries = changedData.Where(entity => entity.State == EntityState.Modified);
            var addedEntries = changedData.Where(entity => entity.State == EntityState.Added);

            foreach (var e in updatedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                }
            }

            foreach (var e in addedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                    entity.CreatedAt = DateTime.Now;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }

}