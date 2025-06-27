
using System;

namespace RobustWindowsService.Domain
{
    public class Convenio : Entity
    {
        public decimal Cupo { get; private set; }
        public DateTime FechaVigencia { get; private set; }
        public ConvenioEstado Estado { get; private set; }

        private Convenio() { } // Constructor privado para forzar el uso del método de fábrica

        public static Convenio Crear(decimal cupo, DateTime fechaVigencia)
        {
            if (cupo <= 0) throw new ArgumentException("El cupo inicial no puede ser cero o negativo.", nameof(cupo));

            var convenio = new Convenio
            {
                Id = Guid.NewGuid(),
                Cupo = cupo,
                FechaVigencia = fechaVigencia,
                Estado = ConvenioEstado.Activo
            };

            convenio.Raise(new ConvenioCreadoEvent(convenio.Id, convenio.Cupo));
            return convenio;
        }

        public void Suspender()
        {
            if (Estado == ConvenioEstado.Finalizado) throw new InvalidOperationException("Un convenio finalizado no puede ser suspendido.");
            if (Estado == ConvenioEstado.Suspendido) return; // Ya está suspendido, no hacer nada.

            Estado = ConvenioEstado.Suspendido;
            Raise(new ConvenioSuspendidoEvent(this.Id));
        }
    }

    public enum ConvenioEstado { Activo, Suspendido, Finalizado }
}
