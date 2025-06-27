
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Worker
{
    public class WorkerCicloCompletadoEvent : IDomainEvent
    {
        public int CicloNumero { get; set; }
    }
}
