
namespace RobustWindowsService.Application
{
    /// <summary>
    /// Interfaz marcadora para un comando que no devuelve un valor de negocio.
    /// Su ejecución representa un cambio de estado.
    /// </summary>
    public interface ICommand { }

    /// <summary>
    /// Interfaz para un comando que devuelve un valor específico tras su ejecución.
    /// </summary>
    public interface ICommand<TResponse> { }
}
