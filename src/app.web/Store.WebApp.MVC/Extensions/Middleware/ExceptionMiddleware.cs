using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);

            }
            catch (BrokenCircuitException)
            {
                HandleSystemUnavailable(httpContext);
            }
            catch (Exception)
            {
                HandleSystemUnavailable(httpContext);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpRequestException)
        {
            if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)httpRequestException.StatusCode;
        }

        private static void HandleSystemUnavailable(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
