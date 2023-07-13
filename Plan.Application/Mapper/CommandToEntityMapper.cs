using Plan.Application.Plan.Commands.CreatePlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Application.Mapper
{
    internal static class CommandToEntityMapper
    {
        internal static Core.Entities.Plan MapToPlan(this CreatePlanCommand command)
        {
            return new Core.Entities.Plan(command.Name, command.InstitutionId.Value, command.AcademicYearId.Value);
        }

        
    }
}
