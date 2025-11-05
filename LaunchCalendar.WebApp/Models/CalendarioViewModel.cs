using System;
using System.Collections.Generic;
using LaunchCalendar.Application.Queries.Filmes;
using LaunchCalendar.Application.Queries.Episodios;
using LaunchCalendar.Application.Queries.Series;

namespace LaunchCalendar.WebApp.ViewModels
{
    public class CalendarioViewModel
    {
        public DateTime InicioSemana { get; set; }
        public IEnumerable<FilmeQueryOutput> Filmes { get; set; } = Array.Empty<FilmeQueryOutput>();
        public IEnumerable<EpisodiosQueryOutput> Episodios { get; set; } = Array.Empty<EpisodiosQueryOutput>();
        public IEnumerable<SerieQueryOutput> Series { get; set; } = Array.Empty<SerieQueryOutput>();
    }
}