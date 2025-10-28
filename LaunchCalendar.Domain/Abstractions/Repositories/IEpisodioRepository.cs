using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Domain.Abstractions.Repositories
{
    public interface IEpisodioRepository
    {
        void Add(Episodio episodio);
        Episodio? GetById(int id);
        IEnumerable<Episodio> GetAll();
        void Update(Episodio filme);
        void Delete(int id);
    }
}
