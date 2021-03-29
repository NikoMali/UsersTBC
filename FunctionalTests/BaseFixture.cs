using Microsoft.AspNetCore.Hosting;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using UsersTBC.WebAPI;

namespace FunctionalTests
{
    public class BaseFixture : WebApplicationFactory<Startup>
    {
        //public HttpClient Client { get; }
        //public TestServer Server { get; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                //services.AddDbContext<DataContext>();
            });
        }
    }
}

