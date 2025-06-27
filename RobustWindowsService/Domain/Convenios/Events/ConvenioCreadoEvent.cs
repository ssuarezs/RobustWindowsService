
using System;

namespace RobustWindowsService.Domain
{
    public class ConvenioCreadoEvent : IDomainEvent
    {
        public Guid ConvenioId { get; }
        public decimal CupoInicial { get; }
        public ConvenioCreadoEvent(Guid convenioId, decimal cupoInicial)
        {
            ConvenioId = convenioId;
            CupoInicial = cupoInicial;
        }
    }
}
