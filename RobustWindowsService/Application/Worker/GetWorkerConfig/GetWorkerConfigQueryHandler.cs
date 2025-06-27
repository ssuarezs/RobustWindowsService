
using System.Threading.Tasks;
using System.Threading;

namespace RobustWindowsService.Application.Worker
{
    public class GetWorkerConfigQueryHandler : IQueryHandler<GetWorkerConfigQuery, WorkerConfigDto>
    {
        public Task<WorkerConfigDto> Handle(GetWorkerConfigQuery query, CancellationToken cancellationToken)
        {
            // Aquí iría la lógica para leer de un App.config, base de datos, etc.
            // Por ahora, devolvemos datos quemados en código como ejemplo.
            if (query.NombreWorker == "ParallelProcessorWorker")
            {
                var config = new WorkerConfigDto
                {
                    IntervaloEnSegundos = 30,
                    NivelDeParalelismo = 4
                };
                return Task.FromResult(config);
            }

            // Devolver null o lanzar una excepción si no se encuentra la config.
            return Task.FromResult<WorkerConfigDto>(null);
        }
    }
}
