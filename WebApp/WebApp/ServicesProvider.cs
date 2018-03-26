using Core.Services.SEG;
using DAOs.SEG;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public partial class Startup
    {
        public void ConfigureProviders(IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioDao>();
            services.AddScoped<IAccountService, AccountServiceProvider>();
        }
    }
}
