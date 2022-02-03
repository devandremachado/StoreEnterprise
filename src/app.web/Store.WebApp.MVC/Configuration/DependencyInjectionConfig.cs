using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Extensions.Interfaces;
using Store.WebApp.MVC.Extensions.Polly;
using Store.WebApp.MVC.Services;
using Store.WebApp.MVC.Services.Interfaces;
using Store.WebApp.MVC.Services.Services;
using Store.WebApp.MVC.Services.Services.Handlers;

namespace Store.Authorization.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegationHandler>();

            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>()
                //.AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
                .AddPolicyHandler(PollyExtensions.RetryWaitPolicy());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }

        
    }
}
