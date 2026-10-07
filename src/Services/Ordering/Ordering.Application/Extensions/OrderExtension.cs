namespace Ordering.Application.Extensions
{
    public static class OrderExtension
    {
        public static IEnumerable<OrderDto> ToOrderListDto(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto
            (
                Id : order.Id.Value,
                CustomerId : order.CustomerId.Value,
                OrderName : order.OrderName.Value,
                ShippingAddress : new AddressDto
                    (
                        order.ShippingAddress.FirstName,
                            order.ShippingAddress.LastName,
                             order.ShippingAddress.Email,

                         order.ShippingAddress.Street,
                         order.ShippingAddress.City,
                         order.ShippingAddress.State,
                         order.ShippingAddress.ZipCode,
                            order.ShippingAddress.Country
                    ),
                BillingAddress : new AddressDto
                    (
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.Email,
                         order.BillingAddress.Street,
                         order.BillingAddress.City,
                         order.BillingAddress.State,
                         order.BillingAddress.ZipCode,
                         order.BillingAddress.Country
                    ),
                Payment : new PaymentDto
                    (
                         order.Payment.CardName,
                         order.Payment.CardNumber,
                         order.Payment.Expiration,
                         order.Payment.CVV,
                         order.Payment.PaymentMethod

                    ),
                Status : order.Status,
                OrderItems : order.OrderItems.Select(oi => new OrderItemDto
                (
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                     oi.UnitPrice,
                    oi.Quantity

                )).ToList()
            ));
        }
    }
}
