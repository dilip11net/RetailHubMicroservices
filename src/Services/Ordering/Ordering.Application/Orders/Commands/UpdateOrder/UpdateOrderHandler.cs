
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDBContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedshippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.Email, orderDto.ShippingAddress.Street, orderDto.ShippingAddress.City, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode, orderDto.ShippingAddress.Country);

            var updatedbillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.Email, orderDto.BillingAddress.Street, orderDto.BillingAddress.City, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode, orderDto.BillingAddress.Country);

            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.ExpirationDate, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updatedshippingAddress,
                billingAddress: updatedbillingAddress,
                payment: updatedPayment,
                status: orderDto.Status
            );
        }
    }
}
