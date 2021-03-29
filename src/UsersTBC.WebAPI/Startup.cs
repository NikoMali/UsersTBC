using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using System;
using UsersTBC.Infrastructure.Middleware;
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

       
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<DataContext>();
            Serilogging.SerilogInitial(_configuration);

            services.AddHttpContextAccessor();
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            settings = appSettingsSection.Get<AppSettings>();

            

            services.AddCors();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            )
            //.AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()))
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }); ;
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            RegisterServices(services);
            services.AddCustomizedSwagger(_env);
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMiddleware<ErrorHandlerMiddleware>();

            }
            else
            {
                app.UseMiddleware<ErrorHandlerMiddleware>();
            }
            app.UseMiddleware<AcceptLanguageHttpHeader>();

           

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
           
            IntarfaceConnReposit.RegisterServices(services);

        }
    }
}
