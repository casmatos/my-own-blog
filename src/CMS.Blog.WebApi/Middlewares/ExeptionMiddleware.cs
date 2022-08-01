using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CMS.Blog.WebApi.Middlewares
{
    public class ExeptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExeptionMiddleware> _logger;

        public ExeptionMiddleware(ILogger<ExeptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                if (exception.InnerException is not null)
                {
                    while (exception.InnerException is not null)
                    {
                        exception = exception.InnerException;
                    }
                }

                if (context is not null)
                {
                    var user = context.User.FindFirst(ClaimTypes.Name) is null ? "Anonymous" : context.User.FindFirst(ClaimTypes.Name)?.Value;

                    _logger.LogError(exception,
                        $"{exception.Message}{Environment.NewLine}HTTP Request Information:{Environment.NewLine}" +
                        $"  Request By: {user}{Environment.NewLine}" +
                        $"  RemoteIP: {context.Connection.RemoteIpAddress}{Environment.NewLine}" +
                        $"  Schema: {context.Request.Scheme}{Environment.NewLine}" +
                        $"  Host: {context.Request.Host}{Environment.NewLine}" +
                        $"  Path: {context.Request.Path}{Environment.NewLine}" +
                        $"  Query String: {context.Request.QueryString}{Environment.NewLine}" +
                        $"  Response Status Code: {context.Response?.StatusCode}{Environment.NewLine}");

                    await next(context);
                }
            }
        }
    }
}
