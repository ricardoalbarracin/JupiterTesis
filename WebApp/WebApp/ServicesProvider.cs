using Core.Business.SEG;
using Core.Services.PARAM;
using Core.Services.SEG;
using DAOs.PARAM;
using DAOs.SEG;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{

    public partial class Startup
    {
        /// <summary>
        /// Metodo usado para definir las clases que implementaran cada uno de los contratos
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProviders(IServiceCollection services)
        {
            services.AddScoped<IUserDAOService, UsuarioDao>();
            services.AddScoped<IsecurityService, SeguridadServiceBusiness>();
            services.AddScoped<IRoleDAOService, RoleDAO>();
            services.AddScoped<IPermissionDAOService, PermisoDAO>();
            services.AddScoped<IPersonDAOService, PersonaDAO>();


        }
    }
}
