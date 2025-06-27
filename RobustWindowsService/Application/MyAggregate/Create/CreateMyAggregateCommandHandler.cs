using System.Threading.Tasks;
using System.Threading;
using System;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Application.Convenios
{
    public class CreateMyAggregateCommandHandler : ICommandHandler<CreateMyAggregateCommand, Guid>
    {
        private readonly IMyAggregateRepository _convenioRepository;

        public CreateMyAggregateCommandHandler(IMyAggregateRepository convenioRepository)
        {
            _convenioRepository = convenioRepository;
        }

        public async Task<Guid> Handle(CreateMyAggregateCommand command, CancellationToken cancellationToken)
        {
            var convenio = MyAggregate.Crear(command.Cupo, command.FechaVigencia);
            await _convenioRepository.AddAsync(convenio);
            return convenio.Id;
        }
    }
}
