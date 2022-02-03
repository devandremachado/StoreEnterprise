using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Extensions.Interfaces;
using Store.WebApp.MVC.Services;
using Store.WebApp.MVC.Services.Interfaces;
using Store.WebApp.MVC.Services.Services;

namespace Store.Authorization.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddHttpClient<ICatalogService, CatalogService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}
