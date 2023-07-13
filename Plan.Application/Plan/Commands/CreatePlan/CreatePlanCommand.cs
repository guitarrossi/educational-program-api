using FluentValidation;
using MediatR;
using Plan.Application.Commom.Models;
using Plan.Application.Interfaces.Repositories;
using Plan.Application.Mapper;

namespace Plan.Application.Plan.Commands.CreatePlan
{
    public record CreatePlanCommand(string Name, Guid? InstitutionId, Guid? AcademicYearId) : IRequest<Response>
    {
    }
    
    public record CreatePlanCommandResponse
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public Guid? InstitutionId { get; init; }

        public Guid? AcademicYearId { get; init; }
    }

    public record CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, Response>
    {
        private readonly IValidator<CreatePlanCommand> _validator;
        private readonly IPlanRepository _planRepository;

        public CreatePlanCommandHandler(IValidator<CreatePlanCommand> validator, IPlanRepository planRepository)
        {
            _validator = validator;
            _planRepository = planRepository;
        }

        public async Task<Response> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = request.MapToPlan();

            await _planRepository.Add(plan);

            var response = new CreatePlanCommandResponse
            {
                AcademicYearId = plan.AcademicPeriodId,
                Id = plan.Id,
                InstitutionId = plan.InstitutionId,
                Name = plan.Name
            };

            return new Response(response);
        }
    }

    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        private readonly IPlanRepository _planRepository;

        public CreatePlanCommandValidator(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public CreatePlanCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MinimumLength(5).WithMessage("Name must have at least 5 characters")
                .MaximumLength(250).WithMessage("Name must have a maximum length of 250 characters");

            RuleFor(c => c.Name)
                .MustAsync(NameBeUnique)
                .WithMessage("Name must be unique");
        }

        private async Task<bool> NameBeUnique(CreatePlanCommand command, string name, CancellationToken cancellation = new CancellationToken())
        {
            return await _planRepository.CheckIfNameIsUnique(name);
        }

    }
}
