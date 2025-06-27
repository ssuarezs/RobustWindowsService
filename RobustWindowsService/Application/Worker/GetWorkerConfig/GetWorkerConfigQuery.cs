    
namespace RobustWindowsService.Application
{   
    public class GetWorkerConfigQuery : IQuery<WorkerConfigDto>
    {
        public string NombreWorker { get; set; }
    }
}
