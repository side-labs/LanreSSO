// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Lanre.Infrastructure.Entities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public static class HttpsExtensions
    {
        public static IServiceCollection AddCustomHttps(this IServiceCollection services, AppSettings appSettings)
        {
            services
                .AddHttpsRedirection(options =>
                 {
                     options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                     options.HttpsPort = appSettings.HttpsConfig.Port;
                 })
                ;
            return services;
        }

        public static IApplicationBuilder UseCustomHttps(this IApplicationBuilder app, IHostingEnvironment env)
        {
            return app
                    .AddIf(!env.IsDevelopment(), x => x.UseHsts())
                    .UseHttpsRedirection()
                ;
        }
    }
}
