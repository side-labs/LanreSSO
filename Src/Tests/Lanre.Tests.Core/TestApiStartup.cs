// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Core
{
    using System.IO;
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

    public class TestApiStartup
    {
        private readonly IHostingEnvironment _currentEnvironment;
        private readonly AppSettings _appSettings;

        public TestApiStartup(IHostingEnvironment env)
        {
            this._currentEnvironment = env;

            var authenticationTestsPath = Directory.GetCurrentDirectory();
            var appJsonPath = Path.GetFullPath(Path.Combine(authenticationTestsPath, "../../../../Lanre.Tests.Core/appsettings.tests.json"));
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(appJsonPath, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                ;

            var builder = configBuilder.Build();
            this._appSettings = builder.Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<AppSettings>(this._appSettings)
                .AddMediatR(Assembly.GetAssembly(typeof(UsersQuery)))
                .ConfigureServicesApi()
                    .AddApplicationPart(typeof(Clients.Api.Controllers.V1.HomeController).Assembly) // Fix for integration tests
                .Services
                .RegisterDataServices(this._appSettings)
                ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .ConfigureApi();
        }
    }
}