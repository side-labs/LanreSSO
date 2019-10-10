// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Host
{
    using System.Reflection;
    using Lanre.Application.Queries.UserQueries;
    using Lanre.Clients.Api;
    using Lanre.Data;
    using Lanre.Infrastructure.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public class Startup
    {
        private readonly IHostingEnvironment _currentEnvironment;
        private readonly AppSettings _appSettings;

        public Startup(IHostingEnvironment env)
        {
            this._currentEnvironment = env;

            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables()
               .AddIf(env.IsDevelopment(), x => x.AddUserSecrets<Startup>(optional: true))
                ;

            var builder = configBuilder.Build();
            this._appSettings = builder.Get<AppSettings>();

            Log.Logger.Warning("settings: {@_appSettings}", this._appSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(this._appSettings)
                .AddMediatR(Assembly.GetAssembly(typeof(UsersQuery)))
                .ConfigureServicesApi()
                .Services
                .RegisterDataServices(this._appSettings)
                .AddCustomHealthChecks(this._appSettings)
                .AddCustomSwagger()
                .AddCustomHttps(this._appSettings)
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app
                .AddIf(this._appSettings.Settings.DetailedErrors, x => x.UseDeveloperExceptionPage())
                .UseCustomErrorHandler()
                .UseCustomHttps(this._currentEnvironment)
                .UseCustomMiniprofilerHandler()
                .UseHealthChecks()
                .ConfigureApi()
                .UseCustomSwagger()
                ;
        }
    }
}
