using System.Threading;
using System.Threading.Tasks;

namespace RobustWindowsService.Application
{
    /// <summary>
    /// Define un manejador para un comando que no devuelve valor.
    /// </summary>
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Define un manejador para un comando que sí devuelve un valor.
    /// </summary>
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
