using ApplicationServices.Services;
using DataAccess.Repositorys;
using Domain.Repositorys;
using Domain.Services;

namespace AplicacaoProjeto.AppConfig
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //services
            services.AddScoped<ICategoriaService, CategoriaService>();
            

            //repositorys
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            

            services.AddScoped<HttpClient>();
            return services;
        }
    }
}
