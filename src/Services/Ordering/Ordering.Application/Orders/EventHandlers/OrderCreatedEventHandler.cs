
namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Handle the OrderCreatedEvent here
            // For example, you can log the event or perform additional actions
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
