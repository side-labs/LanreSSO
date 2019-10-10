// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using global::HealthChecks.UI.Client;
    using Lanre.Infrastructure.Entities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, AppSettings appSettings)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder
                .AddSqlServer(
                    appSettings.ConnectionStrings.Lanre,
                    name: "LanreDB-check",
                    tags: new string[] { "lanredb" });
            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            return app
            .UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            })
             .UseHealthChecks("/liveness", new HealthCheckOptions
             {
                 Predicate = r => r.Name.Contains("self"),
             });
        }
    }
}
