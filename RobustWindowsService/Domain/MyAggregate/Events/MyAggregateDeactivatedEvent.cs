
using System;

namespace RobustWindowsService.Domain
{
    public class MyAggregateDeactivatedEvent : IDomainEvent
    {
        public Guid ConvenioId { get; }

        public MyAggregateDeactivatedEvent(Guid convenioId)
        {
            ConvenioId = convenioId;
        }
    }
}
