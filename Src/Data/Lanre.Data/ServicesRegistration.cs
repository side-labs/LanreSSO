// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Data
{
    using System;
    using Domain.Users;
    using Lanre.Data.Contexts.Lanre;
    using Lanre.Data.Repositories;
    using Lanre.Data.Repositories.Core;
    using Lanre.Infrastructure.Entities;
    using Lanre.Infrastructure.Repository;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, AppSettings settings)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .RegisterRepositories()
                .RegisterReadOnlyRepositories()
                .RegisterDB(settings)
                ;

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryReadOnly<User, Guid>, UserRepository>();
            return services;
        }

        private static IServiceCollection RegisterReadOnlyRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User, Guid>, UserRepository>();
            return services;
        }

        private static IServiceCollection RegisterDB(this IServiceCollection services, AppSettings settings)
        {
            LanreContextInitializer.Instance.ConfigureDB(
                services,
                settings.ConnectionStrings.Lanre,
                "lanre",
                settings).Wait();
            return services;
        }
    }
}
