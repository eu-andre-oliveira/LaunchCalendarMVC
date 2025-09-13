using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Domain.Abstractions.Repositories
{
    public interface ISerieRepository
    {
        void Add(Serie filme);
        Serie? GetById(int id);
        IEnumerable<Serie> GetAll();
        void Update(Serie filme);
        void Delete(int id);
    }
}
