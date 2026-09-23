using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvent;
}
