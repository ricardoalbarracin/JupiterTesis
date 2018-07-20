using Core.Models.Utils;
using Core.Services.Utils;
using DAOs;
using DAOs.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Fliters;

namespace WebApp
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                .Build();

            // Add application services.
            services.AddKendo();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddCors();

            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(new LoginActionFilter());
            })
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
   
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.LogoutPath = "/Account/LogOff";
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            ConfigureProviders(services);
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDapperAdapter, DapperAdapter>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
            app.UseCors(
                options => options.WithOrigins("https://localhost:44330").AllowAnyMethod()
            );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Scripts")),
                RequestPath = "/Scripts"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Areas/SEG/Scripts")),
                RequestPath = "/Areas/SEG/Scripts"
            });
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
            //    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Areas/TRANS/Scripts")),
            //    RequestPath = "/Areas/TRANS/Scripts"
            //});
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Areas/PARAM/Scripts")),
                RequestPath = "/Areas/PARAM/Scripts"
            });

        }
    }
}
