using Cadastro.Domain.Interfaces;
using Cadastro.Domain.Notificacoes;
using Cadastro.Domain.Services;
using Cadastro.Infra.Context;
using Cadastro.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Cadastro.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<UsuarioContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            return services;
        }
    }
}
