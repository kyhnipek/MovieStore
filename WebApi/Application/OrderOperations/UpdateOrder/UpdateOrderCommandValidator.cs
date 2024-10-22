using FluentValidation;

namespace WebApi.Application.OrderOperations.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(query => query.OrderId).GreaterThan(0);

    }
}