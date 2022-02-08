using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Application.Paging.Services;
using UsersTBC.Application.ApplicationDbContext;
using UsersTBC.Infrastructure.Helpers;
using UsersTBC.Infrastructure.Repository;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Linq;
using UsersTBC.Infrastructure.Logging;
using UsersTBC.Infrastructure.Data;

namespace UsersTBC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>();

            Serilogging.SerilogInitial(configuration);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));



            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            ////
            services.AddScoped<ValidateEntityExistsAttribute<User>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<DataContext>());
            //services.AddScoped<IRepository<UserReference>, Repository<UserReference>>();
            //services.AddScoped<IUserService, UserService>();

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



            return services;
        }
    }
}
