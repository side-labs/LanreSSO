// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;

    public static class ApiVersionExtensions
    {
        public static IServiceCollection AddCustomApiVersion(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = ApiVersionReader.Combine(
                                            new QueryStringApiVersionReader("api-version"),
                                            new HeaderApiVersionReader("api-version"));
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }
    }
}