using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventID => Guid.NewGuid();

        public DateTime OccurredOn => DateTime.Now;

        string EventType => GetType().AssemblyQualifiedName;
    }
}
