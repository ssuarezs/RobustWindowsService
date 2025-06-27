using System;
using System.ServiceProcess;
using System.Threading;

namespace RobustWindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // La magia está aquí: Environment.UserInteractive devuelve 'true' si la aplicación
            // fue iniciada por un usuario desde la consola o Visual Studio.
            if (Environment.UserInteractive)
            {
                // Si es interactivo, lo ejecutamos en modo de depuración.
                using (var service = new MainService())
                {
                    Console.WriteLine("Iniciando servicio en modo de depuración...");

                    // Llamamos a nuestro método de ayuda.
                    service.DebugRun();

                    Thread.Sleep(System.Threading.Timeout.Infinite);

                    //Console.WriteLine("Servicio iniciado. Presiona Enter para detener.");
                    //Console.ReadLine(); // Esperamos a que el usuario presione Enter.

                    //Console.WriteLine("Deteniendo el servicio...");
                    //service.Stop(); // Simulamos la detención del servicio.
                    //Console.WriteLine("Servicio detenido.");
                }
            }
            else
            {
                // Si no es interactivo, significa que fue iniciado por el SCM de Windows.
                // Lo ejecutamos como un servicio normal.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MainService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
