using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Plan.Core.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plan.Application.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Plan.Infraestructure.Interceptors
{
    public class AuditableEntityInterceptors : SaveChangesInterceptor
    {
        private readonly ILoggedUser _loggedUser;
        private readonly INow _now;

        public AuditableEntityInterceptors(
            ILoggedUser user,
            INow dateTime)
        {
            _loggedUser = user;
            _now = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added && _loggedUser.Id is not null)
                {
                    entry.Entity.CreatedBy = Guid.Parse(_loggedUser.Id);
                    entry.Entity.CreatedAt = _now.Now;
                }

                if (_loggedUser.Id is not null && (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities()))
                {
                    entry.Entity.UpdatedBy = Guid.Parse(_loggedUser.Id);
                    entry.Entity.UpdatedAt = _now.Now;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
