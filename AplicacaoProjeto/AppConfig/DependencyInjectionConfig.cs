using ApplicationServices;
using DataAccess.Repositorys;
using Domain.Repositorys_interfaces_;
using Domain.Services_Interfaces_;

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
