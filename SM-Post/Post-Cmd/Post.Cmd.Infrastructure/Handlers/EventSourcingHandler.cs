
using CQRS.Core.Domain;
using CQRS.Core.Handler;
using Post.Cmd.Domain.Aggregate;
using CQRS.Core.Infrastructure;

namespace Post.Cmd.Infrastructure.Handler {
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        public readonly IEventStore _eventStore;
        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
        {
           var aggregate = new PostAggregate();
           var events = await _eventStore.GetEventsAsync(aggregateId);
           
           if(events == null || !events.Any()) return aggregate;

           aggregate.ReplayEvent(events);
           var latestVersion = events.Select(x=>x.Version).Max();
           aggregate.Version = latestVersion;
           return aggregate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id,aggregate.GetUnCommittedChanges(),aggregate.Version);
            aggregate.MarkChangesAsCommitted();
        }
    }
}