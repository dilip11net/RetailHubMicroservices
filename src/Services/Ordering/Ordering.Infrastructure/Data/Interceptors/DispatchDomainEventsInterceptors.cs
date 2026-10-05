using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptors(IMediator mediator) : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext context)
        {
           if(context == null) return;

           var aggregates = context.ChangeTracker.Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .Select(x=>x.Entity);

            var domainEntities = aggregates
                .SelectMany(x => x.DomainEvents)
                .ToList();

            aggregates.ToList().ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in domainEntities)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
