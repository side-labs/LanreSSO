// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Serialization;

    public static class ConfigurationApi
    {
        public static IMvcCoreBuilder ConfigureServicesApi(this IServiceCollection services) // , bool addAuthorization = false)
        {
            return services
                .AddCustomApiVersion()
                .AddMvcCore()
                .AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver())

                // .AddIf(addAuthorization, x => x.AddAuthorization())
                .AddApiExplorer()
                ;
        }

        public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app)
        {
            return app.UseMvc(routes => routes.MapRoute("swagger", "{controller=Home}/{action=Swagger}"));
        }
    }
}
