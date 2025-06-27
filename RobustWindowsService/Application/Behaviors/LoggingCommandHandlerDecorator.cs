
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace RobustWindowsService.Application
{
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> 
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _innerHandler;
        private readonly ILoggingService _logger;

        public LoggingCommandHandlerDecorator(
            ICommandHandler<TCommand> innerHandler,
            ILoggingService logger)
        {
            _innerHandler = innerHandler;
            _logger = logger;
        }

        public async Task Handle(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;

            // Lógica ANTES de la ejecución del handler real
            _logger.LogInfo($"--- [FIN] Ejecución de: {commandName} ---");

            try
            {
                // Llama al handler real (el "inner" o "decorado")
                await _innerHandler.Handle(command, cancellationToken);
            }
            finally
            {
                // Lógica DESPUÉS de la ejecución
                _logger.LogInfo($"--- [FIN] Ejecución de: {commandName} ---");
            }
        }
    }
}
