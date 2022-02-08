using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UsersTBC.WebAPI.StartUpExtensions
{
    public static class SwaggerApp
    {
        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseSwagger();
                
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users TBC v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Users TBC v2");

                });
            }

            return app;
        }
    }
}
