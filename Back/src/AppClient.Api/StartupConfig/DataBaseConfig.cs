using AppClient.Repository;
using Microsoft.EntityFrameworkCore;

namespace AppClient.Api.StartupConfig
{
    public static class DataBaseConfig
    {
        public static void AddDataBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("MyDb");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(connection));
        }

        public static void UseDataBaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context?.Database.Migrate();
            context?.Database.EnsureCreated();
        }
    }
}
