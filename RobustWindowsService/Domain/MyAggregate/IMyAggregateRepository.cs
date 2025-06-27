using System;
using System.Threading.Tasks;

namespace RobustWindowsService.Domain
{
    public interface IMyAggregateRepository
    {
        Task AddAsync(MyAggregate convenio);
        Task<MyAggregate> GetByIdAsync(Guid id);
    }
}
