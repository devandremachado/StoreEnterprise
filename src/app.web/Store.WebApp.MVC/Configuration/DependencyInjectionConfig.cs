using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Extensions.Interfaces;
using Store.WebApp.MVC.Services;
using Store.WebApp.MVC.Services.Interfaces;

namespace Store.Authorization.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}
