using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Infraestructure.EntityConfiguration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Core.Entities.Plan>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Plan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
