
using RobustWindowsService.Application.Worker.Events;
using RobustWindowsService.Domain;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace RobustWindowsService.Application.Worker.LogCiclo
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
