using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.EventHandlers
{
  
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            // Handle the OrderUpdatedEvent here
            // For example, you can log the event or perform additional actions
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
