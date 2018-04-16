using Core.Providers.SEG;
using Core.Services.ADMIN;
using Core.Services.SEG;
using DAOs.ADMIN;
using DAOs.SEG;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public partial class Startup
    {
        public void ConfigureProviders(IServiceCollection services)
        {
            services.AddScoped<IUsuarioDAOService, UsuarioDao>();
            services.AddScoped<ISeguridadService, SeguridadServiceProvider>();
            services.AddScoped<IRoleDAOService, RoleDAO>();
            services.AddScoped<IPermisoDAOService, PermisoDAO>();
            services.AddScoped<IPersonaService, PersonaDAO>();
        }
    }
}
