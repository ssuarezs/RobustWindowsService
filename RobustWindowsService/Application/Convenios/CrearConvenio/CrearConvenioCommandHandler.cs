using System.Threading.Tasks;
using System.Threading;
using System;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Convenios
{
    public class CrearConvenioCommandHandler : ICommandHandler<CrearConvenioCommand, Guid>
    {
        private readonly IConvenioRepository _convenioRepository;

        public CrearConvenioCommandHandler(IConvenioRepository convenioRepository)
        {
            _convenioRepository = convenioRepository;
        }

        public async Task<Guid> Handle(CrearConvenioCommand command, CancellationToken cancellationToken)
        {
            var convenio = Convenio.Crear(command.Cupo, command.FechaVigencia);
            await _convenioRepository.AddAsync(convenio);
            return convenio.Id;
        }
    }
}
