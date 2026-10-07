

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;

        public OrderName OrderName { get; private set; } = default!;

        public Address ShippingAddress { get; private set; } = default!;

        public Address BillingAddress { get; private set; } = default!;

        public Payment Payment { get; private set; } = default!;

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalAmount
        {

            get => OrderItems.Sum(item => item.UnitPrice * item.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            //ArgumentNullException.ThrowIfNull(id, nameof(id));
            //ArgumentNullException.ThrowIfNull(customerId, nameof(customerId));
            //ArgumentNullException.ThrowIfNull(orderName, nameof(orderName));
            //ArgumentNullException.ThrowIfNull(shippingAddress, nameof(shippingAddress));
            //ArgumentNullException.ThrowIfNull(billingAddress, nameof(billingAddress));
            //ArgumentNullException.ThrowIfNull(payment, nameof(payment));
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            //ArgumentNullException.ThrowIfNull(orderName, nameof(orderName));
            //ArgumentNullException.ThrowIfNull(shippingAddress, nameof(shippingAddress));
            //ArgumentNullException.ThrowIfNull(billingAddress, nameof(billingAddress));
            //ArgumentNullException.ThrowIfNull(payment, nameof(payment));
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal unitPrice)
        {
            //ArgumentNullException.ThrowIfNull(productId, nameof(productId));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(unitPrice, nameof(unitPrice));
            var orderItem = new OrderItem(Id, productId, quantity, unitPrice);
            _orderItems.Add(orderItem);
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Remove(ProductId productId)
        {
            ArgumentNullException.ThrowIfNull(productId, nameof(productId));
            var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
            if (orderItem is null)
            {
                throw new InvalidOperationException($"Order item with product id {productId} not found.");
            }
            _orderItems.Remove(orderItem);
            AddDomainEvent(new OrderUpdatedEvent(this));

        }
    }
}
