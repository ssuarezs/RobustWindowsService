
using System.Threading.Tasks;
using System.Threading;
using System;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Worker
{
    public class LogCicloHandler : IDomainEventHandler<WorkerCicloCompletadoEvent>
    {
        public Task Handle(WorkerCicloCompletadoEvent domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine($"--- [EVENTO] El Worker ha completado el ciclo número {domainEvent.CicloNumero}. ---");
            return Task.CompletedTask;
        }
    }
}
