using DevFreela.Application.Services.Project;
using DevFreela.Application.Services.ProjectServices;
using DevFreela.Application.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application
{
    //Esta classe será o unico ponto de contato entre o projeto application e o projeto api.
    //Ao inves de adicionar todos os sevices do projeto na classe program, este arquivo será responsavel por isso.
    public static class ApplicationModule
    {
        

        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddServices();
            return services;

        }

        private static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService,UserService>();
            return services;
        }
    }
}
