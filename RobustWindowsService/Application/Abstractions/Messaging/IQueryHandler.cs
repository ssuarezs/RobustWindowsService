using System.Threading;
using System.Threading.Tasks;

namespace RobustWindowsService.Application
{
    /// <summary>
    /// Define un manejador para una consulta. Su única responsabilidad es obtener datos.
    /// </summary>
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
