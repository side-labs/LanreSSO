// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Host.Infrastructure.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class MiniprofilerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiniprofilerMiddleware> _logger;

        public MiniprofilerMiddleware(RequestDelegate next, ILogger<MiniprofilerMiddleware> logger)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // var profiler = MiniProfiler.StartNew("Miniprofiler");
            // using (profiler.Step("Request"))
            // {
                // httpContext.Response.OnStarting(
                //     state =>
                //     {
                //         var httpContext = (HttpContext)state;
                //         profiler.Stop();
                //         httpContext.Response.Headers.Add("X-Response-Time", new[] { profiler.RenderPlainText().Replace("\n", " ").Replace("\r", " ").ToString() });
                //         return Task.FromResult(0);
                //     }, httpContext);

                await this._next(httpContext);
            // }

            // this._logger.LogInformation(profiler.RenderPlainText());
        }
    }
}
