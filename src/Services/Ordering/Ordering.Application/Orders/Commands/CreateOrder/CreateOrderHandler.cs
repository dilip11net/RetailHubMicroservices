

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDBContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // Handle the command here
           
            var order = CreateNewOrder(command.Order);

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress =  Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,  orderDto.ShippingAddress.Email, orderDto.ShippingAddress.Street, orderDto.ShippingAddress.City, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode, orderDto.ShippingAddress.Country);

            var billingAddress =  Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.Email, orderDto.BillingAddress.Street, orderDto.BillingAddress.City, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode, orderDto.BillingAddress.Country);

            var newOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDto.CustomerId),
                OrderName.Of(orderDto.OrderName),
                shippingAddress,
                billingAddress,
                Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.ExpirationDate, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
            );

            foreach (var item in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.UnitPrice);
            }

            return newOrder;
        }
    }
}
