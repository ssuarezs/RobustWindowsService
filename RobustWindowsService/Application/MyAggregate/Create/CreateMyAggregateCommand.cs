using System;

namespace RobustWindowsService.Application.Convenios
{
    public class CreateMyAggregateCommand : ICommand<Guid>
    {
        public decimal Cupo { get; set; }
        public DateTime FechaVigencia { get; set; }
    }
}
