
using Microsoft.Extensions.DependencyInjection;
using RobustWindowsService.Domain;
using System.Threading.Tasks;
using System.Threading;
using System;

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

                    // Usamos la reflexión para invocar el método Handle de forma genérica
                    await (Task)handler.GetType()
                                       .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.Handle))
                                       ?.Invoke(handler, new object[] { domainEvent, cancellationToken });
                }
            }
        }
    }
}
