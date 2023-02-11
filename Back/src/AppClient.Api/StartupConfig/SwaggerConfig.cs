using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AppClient.Api.StartupConfig
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AppCliente Api",
                    Version = "v1",
                    Description = "AppCliente App",
                    Contact = new OpenApiContact
                    {
                        Name = "Renan Luiz Blasechi",
                        Email = "renanluiz2241@gmail.com"
                    }
                });
                c.EnableAnnotations();
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autoglass v1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
