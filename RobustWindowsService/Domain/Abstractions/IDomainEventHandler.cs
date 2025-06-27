
using System.Threading.Tasks;
using System.Threading;

namespace RobustWindowsService.Domain
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
    }
}
