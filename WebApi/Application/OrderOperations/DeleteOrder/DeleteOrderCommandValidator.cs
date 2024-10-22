using FluentValidation;

namespace WebApi.Application.OrderOperations.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(query => query.OrderId).GreaterThan(0);
    }
}