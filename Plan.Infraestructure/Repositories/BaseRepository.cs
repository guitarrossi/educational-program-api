using Microsoft.EntityFrameworkCore;
using Plan.Core.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Infraestructure.Repositories
{
    public abstract class BaseRepository<TEntity>  where TEntity : BaseEntity
    {
        protected DbSet<TEntity> DbSet { get; set; }

        protected BaseRepository(DbContext context)
        {
            DbSet = context.Set<TEntity>();
        }

    }
}
