// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Core
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    public class ServerFactory
    {
        public static TestServer Server(Type startup, Action<IServiceCollection> configureServices = null)
        {
            var webHost = new WebHostBuilder()
                .UseStartup(startup)
                ;
            if (configureServices != null)
            {
                webHost = webHost.ConfigureServices(configureServices);
            }

            var path = Directory.GetCurrentDirectory();
            var applicationPath = Path.GetFullPath(Path.Combine(path, "../../../../../Clients/Lanre.Clients.Api/"));

            webHost.UseContentRoot(applicationPath)
                .UseEnvironment("Development");

            return new TestServer(webHost);
        }
    }
}