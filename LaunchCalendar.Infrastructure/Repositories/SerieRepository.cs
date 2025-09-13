using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.Infrastructure.Data;

namespace LaunchCalendar.Infrastructure.Repositories
{
    public class SerieRepository : ISerieRepository
    {
        private readonly ApplicationDbContext _context;

        public SerieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Serie serie)
        {
            _context.Series.Add(serie);
            _context.SaveChanges();
        }

        public Serie? GetById(int id)
        {
            return _context.Series.Find(id);
        }

        public IEnumerable<Serie> GetAll()
        {
            return _context.Series.ToList();
        }

        public void Update(Serie serie)
        {
            _context.Series.Update(serie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var serie = GetById(id);
            if (serie != null)
            {
                _context.Series.Remove(serie);
                _context.SaveChanges();
            }
        }
    }
}
