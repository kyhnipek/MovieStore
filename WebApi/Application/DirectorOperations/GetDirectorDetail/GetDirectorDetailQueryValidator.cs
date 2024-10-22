using FluentValidation;

namespace WebApi.Application.DirectorOperations.GetDirectorDetail;

public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
{
    public GetDirectorDetailQueryValidator()
    {
        RuleFor(query => query.DirectorId).GreaterThan(0);
    }
}
