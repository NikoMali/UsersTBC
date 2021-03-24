using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Application.Paging.Services;
using UsersTBC.Application.ApplicationDbContext;
using UsersTBC.Insfrastructure.Helpers;


namespace UsersTBC.Insfrastructure.IntarfaceConnReposit
{
    public class IntarfaceConnReposit
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IRepository<UserReference>, Repository<UserReference>>();
           

            //for paging
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            ////
            
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<DataContext>());
        }
    }
}
