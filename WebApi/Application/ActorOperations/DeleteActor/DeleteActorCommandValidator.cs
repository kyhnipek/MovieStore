using FluentValidation;

namespace WebApi.Application.ActorOperations.DeleteActor;

public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
{
    public DeleteActorCommandValidator()
    {
        RuleFor(command => command.ActorId).GreaterThan(0);
    }
}