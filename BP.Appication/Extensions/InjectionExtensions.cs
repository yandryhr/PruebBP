using BP.Appication.Interfaces;
using BP.Appication.Services;
using BP.Appication.Validators.Account;
using BP.Appication.Validators.Movement;
using BP.Application.Interfaces;
using BP.Application.Services;
using BP.Application.Validators.Client;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BP.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(options =>
            options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => p.IsDynamic))
            );        

            services.AddScoped<IClientApplication, ClientApplication>();
            services.AddScoped<IAccountApplication, AccountApplication>();
            services.AddScoped<IMovementApplication, MovementApplication>();

            services.AddValidatorsFromAssembly(typeof(ClientValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(AccountValidator).Assembly);
            services.AddValidatorsFromAssembly(typeof(MovementValidator).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
