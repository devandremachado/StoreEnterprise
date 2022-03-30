using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Store.WebAPI.Service.User;
using Store.WebAPI.Service.User.Interfaces;
using Store.WebApp.MVC.Extensions.AnnotationAttributes;
using Store.WebApp.MVC.Extensions.Polly;
using Store.WebApp.MVC.Services.Interfaces;
using Store.WebApp.MVC.Services.Services;
using Store.WebApp.MVC.Services.Services.Handlers;
using System;

namespace Store.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CPFValidationAttributeAdapterProvider>();

            services.AddTransient<HttpClientAuthorizationDelegationHandler>();

            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>() //Intercepta o SendAsync do HttpClient para adicionar o header jwt
                .AddPolicyHandler(PollyExtensions.RetryWaitPolicy())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            return services;
        }


    }
}
