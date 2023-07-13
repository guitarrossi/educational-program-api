using MediatR;
using Plan.Core.Events.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Application.Plan.EventHandlers
{
    public class PlanStatusChangedEventHandler : INotificationHandler<PlanStatusChangedEvent>
    {
        public Task Handle(PlanStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
