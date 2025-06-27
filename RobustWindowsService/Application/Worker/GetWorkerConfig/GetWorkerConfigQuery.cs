
namespace RobustWindowsService.Application.Worker
{   
    public class GetWorkerConfigQuery : IQuery<WorkerConfigDto>
    {
        public string NombreWorker { get; set; }
    }
}
