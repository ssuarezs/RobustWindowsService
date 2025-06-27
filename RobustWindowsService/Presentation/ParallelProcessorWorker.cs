using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using RobustWindowsService.Application;
using RobustWindowsService.Application.Worker;

namespace RobustWindowsService.Presentation
{
    public class ParallelProcessorWorker : IWorker
    {
        private readonly ILogger<ParallelProcessorWorker> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        private Task _executingTask;
        private CancellationTokenSource _cts;

        public ParallelProcessorWorker(
            ILogger<ParallelProcessorWorker> logger,
            IEventDispatcher eventDispatcher)
        {
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        public Task StartAsync(CancellationToken serviceShutdownToken)
        {
            _logger.LogInformation("Parallel Processor Worker iniciando...");

            // Creamos un CancellationTokenSource interno que está vinculado al token de apagado del servicio.
            // Esto nos permite detener el worker internamente si es necesario, sin afectar al resto del servicio.
            _cts = CancellationTokenSource.CreateLinkedTokenSource(serviceShutdownToken);

            // Task.Run() es CRUCIAL. Lanza el trabajo a un hilo del ThreadPool,
            // para que el método OnStart del servicio no se quede bloqueado esperando.
            // Si OnStart no retorna rápidamente, Windows pensará que el servicio falló.
            _executingTask = Task.Run(async () =>
            {
                // Este es el bucle principal de nuestro worker.
                int cicloActual = 0;

                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("Worker está procesando una tarea. Hora: {time}", DateTimeOffset.Now);
                        await Task.Delay(5000, _cts.Token);
                        await _eventDispatcher.DispatchAsync(new WorkerCicloCompletadoEvent { CicloNumero = cicloActual }, _cts.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        // Esta excepción es ESPERADA cuando se detiene el servicio.
                        // Simplemente salimos del bucle. No es un error.
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Ocurrió un error inesperado en el worker. Reintentando en 10 segundos.");
                        // Para ser resilientes, en caso de un error, esperamos un tiempo antes de continuar el bucle.
                        await Task.Delay(10000);
                    }
                }

                _logger.LogInformation("El bucle de procesamiento del worker ha finalizado.");

            }, _cts.Token);

            // Devolvemos una tarea completada para que el que llama no tenga que esperar.
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parallel Processor Worker deteniéndose...");

            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Señalizamos la cancelación a nuestro bucle.
                _cts.Cancel();
            }
            finally
            {
                // Esperamos a que la tarea (_executingTask) realmente termine.
                // Esto asegura un apagado ordenado (graceful shutdown).
                // Le pasamos un timeout por si algo sale mal y la tarea nunca termina.
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
                _logger.LogInformation("Parallel Processor Worker detenido.");
            }
        }
    }
}
