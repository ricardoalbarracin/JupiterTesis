using Core.Business.SEG;
using Core.Services.PARAM;
using Core.Services.TRANS;
using Core.Services.SEG;
using DAOs.PARAM;
using DAOs.TRANS;
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
            services.AddScoped<IUsuarioDAOService, UsuarioDao>();
            services.AddScoped<ISeguridadService, SeguridadServiceBusiness>();
            services.AddScoped<IRoleDAOService, RoleDAO>();
            services.AddScoped<IPermisoDAOService, PermisoDAO>();
            services.AddScoped<IPersonaDAOService, PersonaDAO>();
            services.AddScoped<IColaboradorDAOService, ColaboradorDAO>();
        }
    }
}
