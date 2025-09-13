using LaunchCalendar.Application.Queries.Filmes; // ajuste o namespace se necess�rio
using LaunchCalendar.Application.UseCases.CadastrarFilme;
using LaunchCalendar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LaunchCalendar.WebApp.Controllers
{
    public class CadastroController : Controller
    {
        // Exemplo: listas em mem�ria (substitua por acesso ao banco de dados conforme necess�rio)
        private static List<Filme> Movies = new();
        private static List<Serie> SeriesList = new();
        private static List<Episodio> Episodes = new();
        private static bool _mocked = false;
        private readonly ICadastrarFilmeUseCase _cadastrarFilmeUseCase;
        private readonly IFilmeQuery _filmeQuery;

        public CadastroController(
            ICadastrarFilmeUseCase cadastrarFilmeUseCase,
            IFilmeQuery filmeQuery
        )
        {
            if (!_mocked)
            {
                // Calcula o in�cio da semana (segunda-feira)
                var hoje = DateTime.Today;
                int diff = hoje.DayOfWeek == DayOfWeek.Sunday
                    ? -6
                    : DayOfWeek.Monday - hoje.DayOfWeek;
                var inicioSemana = hoje.AddDays(diff);

                // Mock de s�ries
                var serie1 = new Serie { SerieId = 1, Titulo = "S�rie A", Genero = "Drama", Episodios = new List<Episodio>() };
                var serie2 = new Serie { SerieId = 2, Titulo = "S�rie B", Genero = "Com�dia", Episodios = new List<Episodio>() };
                SeriesList.AddRange(new[] { serie1, serie2 });

                // Mock de filmes (um na ter�a, outro no s�bado da semana exibida)
                Movies.Add(new Filme
                {
                    FilmeId = 1,
                    Titulo = "Filme X",
                    Genero = "A��o",
                    DataLancamento = inicioSemana.AddDays(1) // Ter�a-feira
                });
                Movies.Add(new Filme
                {
                    FilmeId = 2,
                    Titulo = "Filme Y",
                    Genero = "Fic��o",
                    DataLancamento = inicioSemana.AddDays(5) // S�bado
                });

                // Mock de epis�dios (distribu�dos na semana)
                var ep1 = new Episodio
                {
                    EpisodioId = 1,
                    Titulo = "O In�cio",
                    Numero = 1,
                    Serie = serie1,
                    SerieId = serie1.SerieId,
                    DataLancamento = inicioSemana // Segunda-feira
                };
                var ep2 = new Episodio
                {
                    EpisodioId = 2,
                    Titulo = "A Reviravolta",
                    Numero = 2,
                    Serie = serie1,
                    SerieId = serie1.SerieId,
                    DataLancamento = inicioSemana.AddDays(3) // Quinta-feira
                };
                var ep3 = new Episodio
                {
                    EpisodioId = 3,
                    Titulo = "O Final",
                    Numero = 1,
                    Serie = serie2,
                    SerieId = serie2.SerieId,
                    DataLancamento = inicioSemana.AddDays(6) // Domingo
                };
                Episodes.AddRange(new[] { ep1, ep2, ep3 });

                // Relacionar epis�dios �s s�ries
                serie1.Episodios.Add(ep1);
                serie1.Episodios.Add(ep2);
                serie2.Episodios.Add(ep3);

                _mocked = true;
            }

            _cadastrarFilmeUseCase = cadastrarFilmeUseCase;
            _filmeQuery = filmeQuery;
        }

        // Filmes
        [HttpGet]
        public IActionResult CadastrarFilme() => View();

        [HttpPost]
        public IActionResult CadastrarFilme(CadastrarFilmeInput movie)
        {
            if (ModelState.IsValid)
            {
                _cadastrarFilmeUseCase.Execute(movie);

                return RedirectToAction("CadastrarFilme");
            }
            return View(movie);
        }

        [HttpGet]
        public IActionResult ListarFilmes()
        {
            var filmes = _filmeQuery.ListarTodos();
            return View(filmes);
        }

        // S�ries
        [HttpGet]
        public IActionResult CadastrarSerie() => View();

        [HttpPost]
        public IActionResult CadastrarSerie(Serie series)
        {
            if (ModelState.IsValid)
            {
                series.SerieId = SeriesList.Count + 1;
                SeriesList.Add(series);
                return RedirectToAction("CadastrarSerie");
            }
            return View(series);
        }

        // Epis�dios
        [HttpGet]
        public IActionResult CadastrarEpisodio() => View();

        [HttpPost]
        public IActionResult CadastrarEpisodio(Episodio episode)
        {
            if (ModelState.IsValid)
            {
                episode.EpisodioId = Episodes.Count + 1;
                Episodes.Add(episode);
                return RedirectToAction("CadastrarEpisodio");
            }
            return View(episode);
        }

        [HttpGet]
        public IActionResult CalendarioSemanal()
        {
            ViewData["Title"] = null;

            // Garante que o calend�rio sempre inclua o dia de hoje (segunda a domingo)
            var hoje = DateTime.Today;
            int diff = hoje.DayOfWeek == DayOfWeek.Sunday
                ? -6
                : DayOfWeek.Monday - hoje.DayOfWeek;
            var inicioSemana = hoje.AddDays(diff);
            var fimSemana = inicioSemana.AddDays(6);

            var filmesSemana = Movies.Where(m => m.DataLancamento >= inicioSemana && m.DataLancamento <= fimSemana).ToList();
            var episodiosSemana = Episodes.Where(e => e.DataLancamento >= inicioSemana && e.DataLancamento <= fimSemana).ToList();

            ViewBag.InicioSemana = inicioSemana;
            ViewBag.Filmes = filmesSemana;
            ViewBag.Episodios = episodiosSemana;
            return View();
        }
    }
}