using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.Infrastructure.Data;

namespace LaunchCalendar.Infrastructure.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
        }

        public Filme? GetById(int id)
        {
            return _context.Filmes.Find(id);
        }

        public IEnumerable<Filme> GetAll()
        {
            return _context.Filmes.ToList();
        }

        public void Update(Filme filme)
        {
            _context.Filmes.Update(filme);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var filme = GetById(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                _context.SaveChanges();
            }
        }
    }
}
