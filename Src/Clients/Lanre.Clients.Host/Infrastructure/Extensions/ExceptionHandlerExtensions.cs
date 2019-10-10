// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.AspNetCore.Builder
{
    using Lanre.Clients.Host.Infrastructure.Middlewares;

    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}