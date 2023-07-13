using FastEndpoints;
using MediatR;
using Plan.Application.Commom.Models;
using Plan.Application.Plan.Commands.CreatePlan;

namespace Plan.WebApi.Endpoints.Plan
{
    public class CreatePlanEndpoint : Endpoint<CreatePlanCommand, Response>
    {
        public IMediator _mediator { get; init; }

        public override void Configure()
        {
            Post("/api/plan");
        }

        public override async Task HandleAsync(CreatePlanCommand req, CancellationToken ct)
        {
            var result = await _mediator.Send(req, ct);
            
            if (result.IsSuccess)
                await SendAsync(result);
            else
            {
                ValidationFailures.AddRange(result.Errors);
                await SendErrorsAsync();
            }
        }
    }
}
