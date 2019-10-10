// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data.Contexts.Core
{
    using System;
    using System.Threading.Tasks;
    using Lanre.Infrastructure.Data;
    using Lanre.Infrastructure.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class ContextInitializer<TContext>
        where TContext : ContextCore
    {
        public async Task ConfigureDB(IServiceCollection services, string connectionString, string dbName, AppSettings settings)
        {
            this.AddToDI(services, connectionString, dbName, settings);
            await this.Initialize(services, connectionString, dbName, settings);
        }

        private void AddToDI(IServiceCollection services, string connectionString, string dbName, AppSettings settings)
        {
            if (settings.Database.UseInMemory)
            {
                services.AddDbContext<TContext>(o => o.UseInMemoryDatabase(dbName));
            }
            else
            {
                services.AddDbContext<TContext>(o => o.UseSqlServer(connectionString));
            }
        }

        private async Task Initialize(IServiceCollection services, string connectionString, string dbName, AppSettings settings)
        {
            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();

            if (settings.Database.UseInMemory)
            {
                optionsBuilder.UseInMemoryDatabase(dbName);
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString, 
                                sqlServerOptionsAction: sqlOptions =>
                                {
                                    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                });
            }

            var context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { optionsBuilder.Options, settings });
            if (settings.Database.Migrate && !settings.Database.UseInMemory)
            {
                if (settings.Database.EnsureDeleted)
                {
                    await context.Database.EnsureDeletedAsync();
                }

                // if (!await context.Database.EnsureCreatedAsync())
                // {
                context.Database.Migrate();
                
                // }
            }
        }
    }
}