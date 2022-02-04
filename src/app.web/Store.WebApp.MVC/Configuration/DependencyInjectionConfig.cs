using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Extensions.Interfaces;
using Store.WebApp.MVC.Extensions.Polly;
using Store.WebApp.MVC.Services;
using Store.WebApp.MVC.Services.Interfaces;
using Store.WebApp.MVC.Services.Services;
using Store.WebApp.MVC.Services.Services.Handlers;
using System;

namespace Store.Authorization.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegationHandler>();

            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>() //Intercepta o SendAsync do HttpClient para adicionar o header jwt
                .AddPolicyHandler(PollyExtensions.RetryWaitPolicy())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }

        
    }
}
