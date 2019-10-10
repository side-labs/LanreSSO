// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.AspNetCore.Builder
{
    using Lanre.Clients.Host.Infrastructure.Middlewares;

    public static class MiniprofilerHandlerExtensions
    {
        public static IApplicationBuilder UseCustomMiniprofilerHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MiniprofilerMiddleware>();
        }
    }
}