using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plan.Core.Entities;
using Plan.Infraestructure.Extensions;
using Plan.Infraestructure.Identity;
using Plan.Infraestructure.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Infraestructure.Context
{
    public class PlanContext : IdentityDbContext<User>
    {
        private readonly IMediator? _mediator;
        private readonly AuditableEntityInterceptors? _auditableEntitySaveChangesInterceptor;

        public PlanContext(DbContextOptions<PlanContext> options) : base(options) { }

        public PlanContext(
            DbContextOptions<PlanContext> options,
            IMediator mediator,
            AuditableEntityInterceptors auditableEntitySaveChangesInterceptor)
            : this(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        public DbSet<Core.Entities.Plan> Plans { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_auditableEntitySaveChangesInterceptor != null)
                optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_mediator != null)
            {
                await _mediator.DispatchDomainEvents(this);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
