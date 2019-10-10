// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Swashbuckle.AspNetCore.Swagger;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
                {
                    setup.DescribeAllParametersInCamelCase();
                    setup.DescribeStringEnumsInCamelCase();
                    setup.SwaggerDoc("v1", new Info
                    {
                        Title = "Lanre Api",
                        Version = "v1",
                    });

                    // setup.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                    // {
                    //     Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    //     Name = "Authorization",
                    //     In = "header",
                    //     Type = "apiKey",
                    // });
                    // setup.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    // {
                    //     { "Bearer", new string[] { } },
                    // });
                    setup.CustomSchemaIds(x => x.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanre");
                          setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                      });
        }
    }
}
