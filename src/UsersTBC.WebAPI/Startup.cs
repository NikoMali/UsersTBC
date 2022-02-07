using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UsersTBC.Infrastructure.Middleware;
using UsersTBC.Insfrastructure.Helpers;
using UsersTBC.Insfrastructure.IntarfaceConnReposit;
using UsersTBC.Insfrastructure.StartUpExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static UsersTBC.Application.Users.Commond.CreateUser.CreateUserCommond;
using UsersTBC.Application.Users.Commond.CreateUser;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using ProductTermsControl.WebAPI.Helpers;

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
            });

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ka-GE")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "ka-GE", uiCulture: "ka-GE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                /*options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    // Order is important, its in which order they will be evaluated
                    new CookieRequestCultureProvider(),
                    new QueryStringRequestCultureProvider()
                };*/
                //^^uncomment when unused accept-language
                var defaultCookieRequestProvider =
                    options.RequestCultureProviders.FirstOrDefault(rcp =>
                        rcp.GetType() == typeof(CookieRequestCultureProvider));
                if (defaultCookieRequestProvider != null)
                    options.RequestCultureProviders.Remove(defaultCookieRequestProvider);

                options.RequestCultureProviders.Insert(0,
                    new CookieRequestCultureProvider()
                    {
                        CookieName = ".AspNetCore.Culture",
                        Options = options
                    });
            });
            ///////

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });


            services.AddMemoryCache();
            services.AddHealthChecks();

            services.AddAutoMapper(typeof(AutoMapperProfile));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCustomizedSwagger(_env);
            RegisterServices(services);
            services.AddMediatR(typeof(CreateUserCommondHandler));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Here is the GUI setup and history storage
            

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

            app.UseCustomizedSwagger(_env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            //Sets Health Check dashboard options
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            

        }
        private static void RegisterServices(IServiceCollection services)
        {
            IntarfaceConnReposit.RegisterServices(services);

        }
    }
}
