using FastEndpoints;
using MediatR;
using Plan.Application.Commom.Models;
using Plan.Application.User.Commands.CreateUser;

namespace Plan.WebApi.Endpoints.User
{
    public class CreateUserEndpoint : Endpoint<CreateUserCommand, Response>
    {
        public IMediator _mediator { get; init; }
        public override void Configure()
        {
            Post("api/user");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserCommand req, CancellationToken ct)
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
