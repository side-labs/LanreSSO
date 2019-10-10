// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Host
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                // Close and flush the log.
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseSerilog((builderContext, config) => CreateSerilogLogger(builderContext, config))
                .UseStartup<Startup>()
            ;

        private static void CreateSerilogLogger(WebHostBuilderContext builderContext, LoggerConfiguration config)
        {
            config
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", "Lanre")
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .WriteTo.Console(
                    LogEventLevel.Information,
                    "{NewLine}[{Timestamp:HH:mm:ss} {Level}-{ConnectionId}] {Message}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.AzureApp(
                    LogEventLevel.Warning,
                    "{NewLine}[{Timestamp:HH:mm:ss} {Level}-{ConnectionId}] {Message}{NewLine}{Exception}")
                ;
        }
    }
}
