using FluentValidation;

namespace WebApi.Application.ActorOperations.UpdateActor;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(command => command.ActorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).NotEmpty();
        RuleFor(command => command.Model.Surname).NotEmpty();
    }
}