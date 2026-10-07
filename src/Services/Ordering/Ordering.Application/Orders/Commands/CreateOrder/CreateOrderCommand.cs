using Ordering.Application.Dtos;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName cannot be empty.");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId cannot be empty.");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order must have at least one item.");
        }
    }

}
