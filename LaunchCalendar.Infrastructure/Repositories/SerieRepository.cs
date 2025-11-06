using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            // include Episodios so detail projection works without extra includes
            return _context.Series
                .Include(s => s.Episodios)
                .FirstOrDefault(s => s.SerieId == id);
        }

        public IQueryable<Serie> GetAll()
        {
            // return IQueryable with Episodes included so higher-level queries can project efficiently
            return _context.Series
                .Include(s => s.Episodios)
                .AsQueryable();
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
