
using System;

namespace RobustWindowsService.Domain
{
    public class ConvenioSuspendidoEvent : IDomainEvent
    {
        public Guid ConvenioId { get; }

        public ConvenioSuspendidoEvent(Guid convenioId)
        {
            ConvenioId = convenioId;
        }
    }
}
