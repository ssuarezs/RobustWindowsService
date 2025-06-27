using System;
using System.IO;
using System.Threading.Tasks;
using RobustWindowsService.Domain;

namespace RobustWindowsService.Infrastructure.Persistence
{
    public class MyAggregateRepositoryTxt : IMyAggregateRepository
    {
        private const string FilePath = "aggregates_list.txt";

        public Task AddAsync(MyAggregate agg)
        {
            string data = $"ID: {agg.Id}, Aggregate: {agg.Cupo}, Estado: {agg.Estado}";
            return Task.Run(() => File.AppendAllText(FilePath, data + Environment.NewLine));
        }

        public Task<MyAggregate> GetByIdAsync(Guid id)
        {
            // Implementación de lectura omitida por simplicidad  
            throw new NotImplementedException();
        }
    }
}
