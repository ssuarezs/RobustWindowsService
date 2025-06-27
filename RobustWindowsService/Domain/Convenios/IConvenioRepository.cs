using System;
using System.Threading.Tasks;

namespace RobustWindowsService.Domain
{
    public interface IConvenioRepository
    {
        Task AddAsync(Convenio convenio);
        Task<Convenio> GetByIdAsync(Guid id);
    }
}
