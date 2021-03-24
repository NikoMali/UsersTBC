using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using System;
using UsersTBC.Insfrastructure.Helpers;
using UsersTBC.Insfrastructure.IntarfaceConnReposit;
using UsersTBC.Insfrastructure.StartUpExtensions;

namespace UsersTBC.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private AppSettings settings;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>();
            Serilogging.SerilogInitial(_configuration);

            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            settings = appSettingsSection.Get<AppSettings>();


            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            RegisterServices(services);
            services.AddCustomizedSwagger(_env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           /* app.UseCors(builder => builder
                       .WithOrigins(settings.AllowedHost)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       );*/

            app.UseHttpsRedirection();

            app.UseRouting();

            dataContext.Database.Migrate();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomizedSwagger(_env);
        }
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            IntarfaceConnReposit.RegisterServices(services);

        }
    }
}
