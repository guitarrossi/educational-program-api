using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Application.Interfaces.Repositories
{
    public interface IPlanRepository
    {
        Task Add(Core.Entities.Plan plan);

        Task<Core.Entities.Plan> GetById(Guid id);

        Task<Core.Entities.Plan> GetByIdAsNoTracking(Guid id);

        Task<bool> CheckIfNameIsUnique(string name);
    }
}
