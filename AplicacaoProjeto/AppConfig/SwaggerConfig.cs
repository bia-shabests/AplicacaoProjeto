using Microsoft.OpenApi.Models;

namespace AplicacaoProjeto.AppConfig
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api Aplicação Projeto",
                        Version = "v1",
                        Description = "Aplicação de estudos ",
                        Contact = new OpenApiContact
                        {
                            Name = "Desenvolvedora Bianca Shabests",
                            Email = "bianca.shabests@hotmail.com"
                        }
                    });
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UsePathBase("/aplicacaoprojeto");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("aplicacaoprojeto/swagger/v1/swagger.json", "Api Aplicacao Projeto");
            });

            return app;
        }
    }
}
