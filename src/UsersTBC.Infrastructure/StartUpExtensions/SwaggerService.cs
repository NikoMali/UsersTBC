using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UsersTBC.Infrastructure.Helpers;

namespace ProductTermsControl.Insfrastructure.StartUpExtensions
{
    public static class SwaggerService
    {
        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "TBC Users",
                        Description = ""
                    });
                    c.DocumentFilter<SwaggerEnumDocumentFilter>();
                    //c.OperationFilter<AddFileParamTypesOperationFilter>();

                    var xmlPath = $@"{env.ContentRootPath}/UsersTBC.WebAPI.xml";
                    
                    c.IncludeXmlComments(xmlPath);
                    
                });
            }

            return services;
        }
    }
}