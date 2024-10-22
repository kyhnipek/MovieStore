using FluentValidation;

namespace WebApi.Application.ActorOperations.GetActorDetail;

public class GetActorDetailQueryValidator : AbstractValidator<GetActorDetailQuery>
{
    public GetActorDetailQueryValidator()
    {
        RuleFor(query => query.ActorId).GreaterThan(0);
    }
}
