using System.Threading;
using System.Threading.Tasks;

namespace RobustWindowsService.Application
{
    public interface IWorker
    {
        /// <summary>
        /// Inicia la ejecución del worker.
        /// </summary>
        /// <param name="cancellationToken">Un token para señalar que la operación debe ser cancelada.</param>
        Task StartAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Señaliza al worker que debe detener su ejecución de forma ordenada.
        /// </summary>
        /// <param name="cancellationToken">Un token para el proceso de apagado.</param>
        Task StopAsync(CancellationToken cancellationToken);
    }
}
