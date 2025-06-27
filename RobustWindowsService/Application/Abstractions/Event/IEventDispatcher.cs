using RobustWindowsService.Domain;
using System.Threading.Tasks;
using System.Threading;

namespace RobustWindowsService.Application
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
