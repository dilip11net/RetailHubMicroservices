using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {

            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Order ID cannot be empty.");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name cannot be empty.");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer ID is Required.");
            
        }
    }
    
}
