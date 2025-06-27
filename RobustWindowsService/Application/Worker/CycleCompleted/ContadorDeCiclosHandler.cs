

using System.Threading.Tasks;
using System.Threading;
using System;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Worker
{
    public class ContadorDeCiclosHandler : IDomainEventHandler<WorkerCicloCompletadoEvent>
    {
        private static int _contadorTotal = 0;
        public Task Handle(WorkerCicloCompletadoEvent domainEvent, CancellationToken cancellationToken)
        {
            _contadorTotal++;
            Console.WriteLine($"--- [EVENTO] El contador total de ciclos ahora es: {_contadorTotal}. ---");
            return Task.CompletedTask;
        }
    }
}
