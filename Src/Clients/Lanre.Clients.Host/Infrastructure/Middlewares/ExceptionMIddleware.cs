// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Clients.Host.Infrastructure.Middlewares
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Lanre.Clients.Host.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._logger = logger;
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await this._next(httpContext);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Internal Server Error from the custom middleware.",
                ExtendedMessage = exception.Message,
                StackTrace = exception.StackTrace,
            }.ToString());
        }
    }
}
