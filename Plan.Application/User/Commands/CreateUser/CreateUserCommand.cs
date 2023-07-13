using Plan.Application.Commom.Models;
using Plan.Application.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Application.User.Commands.CreateUser
{
    public record CreateUserCommand(string Username, string Email, string PhoneNumber, string Password) : IRequest<Response>
    {

    }

    public record CreateUserCommandResponse
    {
        public Guid UserId { get; set; }

    }

    public record CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.CreateUserAsync(request.Username, request.Password);

            return new Response(new CreateUserCommandResponse { UserId = Guid.NewGuid() });
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty();

            RuleFor(u => u.Username)
                .NotEmpty();
        }
    }
}


