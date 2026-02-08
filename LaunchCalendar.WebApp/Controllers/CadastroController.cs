using LaunchCalendar.Application.Queries.Filmes;
using LaunchCalendar.Application.Queries.Episodios;
using LaunchCalendar.Application.Queries.Series;
using LaunchCalendar.Application.UseCases.CadastrarFilme;
using LaunchCalendar.Application.UseCases.CadastrarSerie;
using LaunchCalendar.Application.UseCases.CadastrarEpisodios;
using LaunchCalendar.Domain.Entities;
using LaunchCalendar.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
        public IActionResult CadastrarSerie(CadastrarSerieInput input)
        {
            if (ModelState.IsValid)
            {
                _cadastrarSerieUseCase.Execute(input);
                return RedirectToAction("CadastrarSerie");
            }
            return View(input);
        }

        // Episódios - GET now accepts optional serieId to preselect
        [HttpGet]
        public IActionResult CadastrarEpisodio(int? serieId = null)
        {
            // populate series list for the select
            ViewBag.SeriesList = _serieQuery.ListarTodos();

            if (serieId.HasValue)
            {
                // pass a model with SerieId preselected so the select is bound
                var preModel = new CadastrarEpisodioInput { SerieId = serieId.Value, DataLancamento = DateTime.Today };
                return View(preModel);
            }

            return View();
        }

        [HttpPost]
        public IActionResult CadastrarEpisodio(CadastrarEpisodioInput input)
        {
            if (ModelState.IsValid)
            {
                _cadastrarEpisodioUseCase.Execute(input);

                // after successful creation redirect to the series detail page
                return RedirectToAction("DetalharSerie", new { id = input.SerieId });
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
            IEnumerable<EpisodiosQueryOutput> episodios = _episodiosQuery.ListarComFiltro(new EpisodiosFilterInput());
            return View(episodios);
        }

        // Calendário semanal - now accepts optional start (yyyy-MM-dd)
        [HttpGet]
        public IActionResult CalendarioSemanal(string? start = null)
        {
            DateTime baseDate;

            if (!string.IsNullOrEmpty(start)
                && DateTime.TryParseExact(start, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
            {
                baseDate = parsed.Date;
            }
            else
            {
                baseDate = DateTime.Today;
            }

            // align baseDate to the Monday of its week
            int diff = baseDate.DayOfWeek == DayOfWeek.Sunday
                ? -6
                : DayOfWeek.Monday - baseDate.DayOfWeek;
            var inicioSemana = baseDate.AddDays(diff);
            var fimSemana = inicioSemana.AddDays(6);

            // build filters for queries
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

        [HttpGet]
        public IActionResult DetalharSerie(int id)
        {
            var detalhe = _serieQuery.ObterPorId(id);
            if (detalhe == null) return NotFound();

            return View(detalhe);
        }

        [HttpGet]
        public IActionResult DetalharEpisodio(int id)
        {
            var episodio = _episodiosQuery.ObterPorId(id);
            if (episodio == null) return NotFound();

            return View(episodio);
        }
    }
}