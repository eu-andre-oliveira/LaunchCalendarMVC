using LaunchCalendar.Application.Queries.Filmes;
using LaunchCalendar.Application.Queries.Series;
using LaunchCalendar.Application.UseCases.CadastrarFilme;
using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Infrastructure.Repositories;

namespace LaunchCalendar.WebApp.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Registrando Repositórios
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<IFilmeQuery, FilmeQuery>();
            services.AddScoped<ISerieRepository, SerieRepository>();
            services.AddScoped<ISerieQuery, SerieQuery>();

            // Registrando Casos de Uso
            services.AddScoped<ICadastrarFilmeUseCase, CadastrarFilmeUseCase>();
        }
    }
}
