using Plan.Core.Commom;
using Plan.Core.Enum;
using Plan.Core.Events.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Core.Entities
{
    public class Plan : BaseAuditableEntity
    {
        protected Plan()
        {
        }

        public Plan(string? name, Guid institutionId, Guid academicPeriodId)
        {
            Name = name;
            InstitutionId = institutionId;
            AcademicPeriodId = academicPeriodId;
        }

        public string? Name { get; private set; }

        public Guid InstitutionId { get; private set; }

        public Guid AcademicPeriodId { get; private set; }

        public PlanStatusEnum Status { get; private set; }

        public void SetStatus(PlanStatusEnum status)
        {

            if (Status != status)
            {
                Status = status;
                AddDomainEvent(new PlanStatusChangedEvent(this));
            }
            
        }

    }
}
