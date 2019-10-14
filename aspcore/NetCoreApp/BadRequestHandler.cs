using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetCoreApp
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BadRequestHandler
    {
        private readonly RequestDelegate _next;

        public BadRequestHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "text/html";
                string ms = string.Format($"<h2> {ex.Message} </h2>\r\n <h3>Error code {httpContext.Response.StatusCode }</h3>");//почему не работает html код
                await httpContext.Response.WriteAsync(ms);
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BadRequestHandlerExtensions
    {
        public static IApplicationBuilder UseBadRequestHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BadRequestHandler>();
        }
    }
}
