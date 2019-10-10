// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Contexts.Lanre
{
    using System.IO;
    using global::Lanre.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class LanreContextDesignFactory : IDesignTimeDbContextFactory<LanreContext>
    {
        public LanreContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.Development.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();

            var appSettings = config.Get<AppSettings>();
            var optionsBuilder = new DbContextOptionsBuilder<LanreContext>();

            optionsBuilder.UseSqlServer(appSettings.ConnectionStrings.Lanre);

            return new LanreContext(optionsBuilder.Options, appSettings);
        }
    }
}