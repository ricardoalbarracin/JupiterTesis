using Core.Business.SEG;
using Core.Business.TRANS;
using Core.Models.TRANS;
using Core.Services.PARAM;
using Core.Services.SEG;
using Core.Services.TRANS;
using DAOs.PARAM;
using DAOs.SEG;
using DAOs.TRANS;
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
            services.AddScoped<ICargoDAOService, CargoDAO>();
            services.AddScoped<IRubroDAOService, RubroDAO>();
            services.AddScoped<IDependenciaDAOService, DependenciaDAO>();
            services.AddScoped<IProyectoDAOService, ProyectoDAO>();
            services.AddScoped<IProyectoServiceBusiness, ProyectosServiceBusiness>();
            services.AddScoped<ITiposDocumentoDAOService, TiposDocumentoDAO>();
            services.AddScoped<ISexoDAOService, SexosDAO>();
            services.AddScoped<IColaboradorComisionDAOService, ComisionColaboradorDAO>();
            services.AddScoped<IDivipolaDAOService, DivipolaDAO>();
        }
    }
}
