using DevFreela.Core.Repositories.ProjectRepositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories.ProjectRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddRepositories()
                .AddData(configuration);

            return services;
        }
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration )
        {
            string conString = configuration.GetConnectionString("DevfreelaCs");
            services.AddDbContext<DevFreelaDbContext>(opts => opts.UseSqlServer(conString));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
