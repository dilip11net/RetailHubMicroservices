using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint, IFeatureManager featureManager) 
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            // Handle the OrderCreatedEvent here
            // For example, you can log the event or perform additional actions
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                logger.LogInformation("Feature 'OrderFullfilment' is enabled. Publishing integration event.");

                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
            
        }
    }
}
