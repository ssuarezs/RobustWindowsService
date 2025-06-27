using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

using RobustWindowsService.Domain;
using RobustWindowsService.Application;
using RobustWindowsService.Application.Worker;
using RobustWindowsService.Infrastructure.Persistence;
using RobustWindowsService.Presentation;

namespace RobustWindowsService
{
    public static class DependencyContainer
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(configure => { configure.AddConsole(); });
            services.AddScoped<IConvenioRepository, ConvenioRepositoryTxt>();

            services.AddSingleton<IWorker, ParallelProcessorWorker>();

            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IEventDispatcher, EventDispatcher>();

            services.Scan(selector => selector
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            // Decoradores de comportamiento
            services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            return services.BuildServiceProvider();
        }
    }
}
