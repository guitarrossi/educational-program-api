
using Plan.Core.Commom;

namespace Plan.Core.Events.Plan
{
    public class PlanStatusChangedEvent : BaseEvent
    {
        public Entities.Plan Plan { get; set; }
        public PlanStatusChangedEvent(Entities.Plan plan)
        {
            Plan = plan;
        }
    }
}
