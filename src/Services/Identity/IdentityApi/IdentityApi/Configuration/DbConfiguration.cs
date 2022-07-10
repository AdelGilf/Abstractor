using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace IdentityApi.Configuration
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(opts =>
        opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            options => options.MigrationsAssembly("Infrastructure")));
            return services;
        }
    }
}
