using Microsoft.EntityFrameworkCore;
using Plan.Application.Interfaces.Repositories;
using Plan.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Infraestructure.Repositories
{
    public class PlanRepository : BaseRepository<Core.Entities.Plan>, IPlanRepository
    {
        public PlanRepository(PlanContext context) : base(context)
        {
        }

        public async Task Add(Core.Entities.Plan plan)
        {
            await DbSet.AddAsync(plan);
        }

        public async Task<bool> CheckIfNameIsUnique(string name)
        {
            var count = await DbSet.CountAsync(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            return count == 0;
        }

        public async Task<Core.Entities.Plan> GetById(Guid id) => await DbSet.FindAsync(id);

        public async Task<Core.Entities.Plan> GetByIdAsNoTracking(Guid id) => await DbSet.Where(p => p.Id == id).AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync();


    }
}
