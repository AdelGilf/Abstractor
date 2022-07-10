using Contracts.DTO;
using Core.Services.Abstraction;
using Core.Services.Implementation;
using Core.Validators;
using FluentValidation;
using Infrastructure.Repository.Abstraction;
using Infrastructure.Repository.Implementation;

namespace IdentityApi.Configuration
{
    public static class CoreConfiguration
    {
        public static IServiceCollection AddCore(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IHashingService, HashingService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
