using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RobustWindowsService.Application
{
    public class StartProcessingCommandHandler : ICommandHandler<StartProcessingCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StartProcessingCommandHandler> _logger;

        public StartProcessingCommandHandler(
            IServiceProvider serviceProvider, 
            ILogger<StartProcessingCommandHandler> logger)
        {
            // Inyectamos el IServiceProvider para poder resolver los servicios que necesitamos.
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task Handle(StartProcessingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler ha recibido el comando para iniciar todos los workers.");

            // ¡Esta es la parte más potente y extensible!
            // Usamos el Service Provider para pedirle TODAS las implementaciones de IWorker
            // que registramos en el DependencyContainer.
            var workers = _serviceProvider.GetServices<IWorker>();

            if (!workers.Any())
            {
                _logger.LogWarning("No se encontraron workers de tipo IWorker registrados. No se iniciará nada.");
                return Task.CompletedTask;
            }

            _logger.LogInformation("Iniciando {WorkerCount} workers registrados...", workers.Count());

            foreach (var worker in workers)
            {
                _logger.LogInformation("...Iniciando worker: {WorkerType}", worker.GetType().Name);

                // Iniciamos cada worker, pasándole el token de cancelación que viene del servicio.
                // Si el servicio se detiene, este token se cancelará y los workers lo sabrán.
                worker.StartAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }
    }
}
