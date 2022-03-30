using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Store.Cart.Infra.Data.Context;
using Store.WebAPI.Service.User;
using Store.WebAPI.Service.User.Interfaces;

namespace Store.Cart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CartContext>();
        }
    }
}
