using System.Linq;
using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Domain.Abstractions.Repositories
{
    public interface ISerieRepository
    {
        void Add(Serie serie);
        Serie? GetById(int id);
        IQueryable<Serie> GetAll();
        void Update(Serie serie);
        void Delete(int id);
    }
}