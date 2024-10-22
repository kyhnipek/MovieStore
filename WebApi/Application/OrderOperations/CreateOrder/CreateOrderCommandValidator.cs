using FluentValidation;

namespace WebApi.Application.OrderOperations.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>

{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Model.MovieIds).NotEmpty();
    }
}