using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace LaunchCalendar.Infrastructure.Repositories
{
    public class EpisodioRepository : IEpisodioRepository
    {
        private readonly ApplicationDbContext _context;

        public EpisodioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Episodio episodio)
        {
            _context.Episodios.Add(episodio);
            _context.SaveChanges();
        }

        public Episodio? GetById(int id)
        {
            return _context.Episodios.Find(id);
        }

        public IEnumerable<Episodio> GetAll() => _context.Episodios
                .Include(x => x.Serie)
                .ToList();

        public void Update(Episodio episodio)
        {
            _context.Episodios.Update(episodio);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var episodio = GetById(id);
            if (episodio != null)
            {
                _context.Episodios.Remove(episodio);
                _context.SaveChanges();
            }
        }
    }
}