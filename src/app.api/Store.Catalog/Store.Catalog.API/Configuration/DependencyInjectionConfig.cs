using Microsoft.Extensions.DependencyInjection;
using Store.Catalog.Domain.Interfaces;
using Store.Catalog.infrastructure.Context;
using Store.Catalog.infrastructure.Repository;

namespace Store.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
