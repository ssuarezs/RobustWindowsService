
using System.Threading.Tasks;
using System.Threading;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
