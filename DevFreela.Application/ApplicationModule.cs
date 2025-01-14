using DevFreela.Application.Models;
using DevFreela.Application.Services.AuthServices;
using DevFreela.Application.Services.Commands.CommandsProject.InsertCommentProject;
using DevFreela.Application.Services.Commands.CommandsProject.InsertProject;
using DevFreela.Application.Services.Commands.CommandsProject.ValidateCommandsProject;
using DevFreela.Core.Services.Auth;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevFreela.Application
{
    //Esta classe será o unico ponto de contato entre o projeto application e o projeto api.
    //Ao inves de adicionar todos os sevices do projeto na classe program, este arquivo será responsavel por isso.
    public static class ApplicationModule
    {
        public static IServiceCollection AddAplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHandlers()
                .AddValidation()
                .AddAuth(configuration);
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {                                        //Vai buscar todos os handlers que estão no mesmo assembly de InserCommentProjectHandler   
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertCommentProjectHandler>());
            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectBehavior>();
            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection sevices)
        {
            sevices.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertProjectCommand>();
                return sevices;
        }


        public static IServiceCollection AddAuth( this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });
            return service;
        }
    }
}
