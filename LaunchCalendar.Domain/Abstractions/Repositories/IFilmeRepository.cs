using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Domain.Abstractions.Repositories
{
    public interface IFilmeRepository
    {
        void Add(Filme filme);
        Filme? GetById(int id);
        IEnumerable<Filme> GetAll();
        void Update(Filme filme);
        void Delete(int id);
    }

}
