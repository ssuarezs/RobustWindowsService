using System;
using System.IO;
using System.Threading.Tasks;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Infrastructure.Persistence
{
    public class ConvenioRepositoryTxt : IConvenioRepository
    {
        private const string FilePath = "convenios.txt";

        public Task AddAsync(Convenio convenio)
        {
            string data = $"ID: {convenio.Id}, Cupo: {convenio.Cupo}, Estado: {convenio.Estado}";
            return Task.Run(() => File.AppendAllText(FilePath, data + Environment.NewLine));
        }

        public Task<Convenio> GetByIdAsync(Guid id)
        {
            // Implementación de lectura omitida por simplicidad  
            throw new NotImplementedException();
        }
    }
}
