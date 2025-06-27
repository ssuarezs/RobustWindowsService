
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using System;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {
                    if (handler is null) continue;

                    await (Task)handler.GetType()
                                       .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.Handle))
                                       ?.Invoke(handler, new object[] { domainEvent, cancellationToken });
                }
            }
        }
    }
}
