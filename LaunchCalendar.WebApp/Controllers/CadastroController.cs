using LaunchCalendar.Application.Queries.Filmes;
using LaunchCalendar.Application.Queries.Episodios;
using LaunchCalendar.Application.Queries.Series;
using LaunchCalendar.Application.UseCases.CadastrarFilme;
using LaunchCalendar.Application.UseCases.CadastrarSerie;
using LaunchCalendar.Application.UseCases.CadastrarEpisodios;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LaunchCalendar.WebApp.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ICadastrarFilmeUseCase _cadastrarFilmeUseCase;
        private readonly ICadastrarSerieUseCase _cadastrarSerieUseCase;
        private readonly ICadastrarEpisodioUseCase _cadastrarEpisodioUseCase;
        private readonly IFilmeQuery _filmeQuery;
        private readonly IEpisodiosQuery _episodiosQuery;
        private readonly ISerieQuery _serieQuery;

        public CadastroController(
            ICadastrarFilmeUseCase cadastrarFilmeUseCase,
            ICadastrarSerieUseCase cadastrarSerieUseCase,
            ICadastrarEpisodioUseCase cadastrarEpisodioUseCase,
            IFilmeQuery filmeQuery,
            IEpisodiosQuery episodiosQuery,
            ISerieQuery serieQuery)
        {
            _cadastrarFilmeUseCase = cadastrarFilmeUseCase;
            _cadastrarSerieUseCase = cadastrarSerieUseCase;
            _cadastrarEpisodioUseCase = cadastrarEpisodioUseCase;
            _filmeQuery = filmeQuery;
            _episodiosQuery = episodiosQuery;
            _serieQuery = serieQuery;
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

        // Séries
        [HttpGet]
        public IActionResult CadastrarSerie() => View();

        [HttpPost]
        public IActionResult CadastrarSerie(LaunchCalendar.Application.UseCases.CadastrarSerie.CadastrarSerieInput input)
        {
            if (ModelState.IsValid)
            {
                _cadastrarSerieUseCase.Execute(input);
                return RedirectToAction("CadastrarSerie");
            }
            return View(input);
        }

        // Episódios
        [HttpGet]
        public IActionResult CadastrarEpisodio()
        {
            // populate series list for the select
            ViewBag.SeriesList = _serieQuery.ListarTodos();
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarEpisodio(LaunchCalendar.Application.UseCases.CadastrarEpisodios.CadastrarEpisodioInput input)
        {
            if (ModelState.IsValid)
            {
                _cadastrarEpisodioUseCase.Execute(input);
                return RedirectToAction("CadastrarEpisodio");
            }

            // repopulate series list when returning the view with validation errors
            ViewBag.SeriesList = _serieQuery.ListarTodos();
            return View(input);
        }

        // List pages
        [HttpGet]
        public IActionResult ListarFilmes()
        {
            var filmes = _filmeQuery.ListarTodos();
            return View(filmes);
        }

        [HttpGet]
        public IActionResult ListarSeries()
        {
            var series = _serieQuery.ListarTodos();
            return View(series);
        }

        [HttpGet]
        public IActionResult ListarEpisodios()
        {
            var episodios = _episodiosQuery.ListarComFiltro(new LaunchCalendar.Application.Queries.Episodios.EpisodiosFilterInput());
            return View(episodios);
        }

        // Calendário semanal
        [HttpGet]
        public IActionResult CalendarioSemanal()
        {
            var hoje = DateTime.Today;
            int diff = hoje.DayOfWeek == DayOfWeek.Sunday ? -6 : DayOfWeek.Monday - hoje.DayOfWeek;
            var inicioSemana = hoje.AddDays(diff);
            var fimSemana = inicioSemana.AddDays(6);

            var filmes = _filmeQuery.ListarComFiltro(new LaunchCalendar.Application.Queries.Filmes.FilmeFilterInput
            {
                DataLancamentoInicio = inicioSemana,
                DataLancamentoFim = fimSemana
            });

            var episodios = _episodiosQuery.ListarComFiltro(new LaunchCalendar.Application.Queries.Episodios.EpisodiosFilterInput
            {
                DataLancamentoInicio = inicioSemana,
                DataLancamentoFim = fimSemana
            });

            var series = _serieQuery.ListarTodos();

            var vm = new CalendarioViewModel
            {
                InicioSemana = inicioSemana,
                Filmes = filmes,
                Episodios = episodios,
                Series = series
            };

            return View(vm);
        }
    }
}