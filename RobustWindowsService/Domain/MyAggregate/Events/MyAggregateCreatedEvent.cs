
using System;

namespace RobustWindowsService.Domain
{
    public class MyAggregateCreatedEvent : IDomainEvent
    {
        public Guid ConvenioId { get; }
        public decimal CupoInicial { get; }
        public MyAggregateCreatedEvent(Guid convenioId, decimal cupoInicial)
        {
            ConvenioId = convenioId;
            CupoInicial = cupoInicial;
        }
    }
}
