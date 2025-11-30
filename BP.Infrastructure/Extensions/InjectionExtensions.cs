using BP.Infrastructure.Persistences.Contexts;
using BP.Infrastructure.Persistences.Interfaces;
using BP.Infrastructure.Persistences.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(BpContext).Assembly.FullName;

            services.AddDbContext<BpContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("BPConnection"), b => b.MigrationsAssembly(assembly)),
                ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
