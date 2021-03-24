using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersTBC.Insfrastructure.Helpers
{
    public static class Serilogging
    {
        public static void SerilogInitial(IConfiguration configuration)
        {
            var k = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "Serilog");
            /*Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration, "Serilog")
            //.MinimumLevel.Information()
            .WriteTo.MSSqlServer(
                connectionString: configuration.GetConnectionString("WebApiDatabase"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" }))
            .CreateLogger();*/
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration, "Serilog")
                        .WriteTo
                        .MSSqlServer(
                            connectionString: configuration.GetConnectionString("WebApiDatabase"),
                            sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true })
                        .CreateLogger();
        }
    }
}
