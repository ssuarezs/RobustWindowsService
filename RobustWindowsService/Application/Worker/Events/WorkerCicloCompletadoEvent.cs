using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Worker.Events
{
    public class WorkerCicloCompletadoEvent : IDomainEvent
    {
        public int CicloNumero { get; set; }
    }
}
